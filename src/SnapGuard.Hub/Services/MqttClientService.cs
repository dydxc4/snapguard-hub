using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Protocol;
using SnapGuard.Hub.Configurations;
using SnapGuard.Hub.Types;
using SnapGuard.Requests;
using SnapGuard.Types;
using SnapGuard.Types.Enums;

namespace SnapGuard.Hub.Services;

public class MqttClientService : IMqttClientService
{
    private readonly IMqttClient _client;
    private readonly IdGenerationService _idGenerator;
    private readonly RequestCorrelationService _correlationService;
    private readonly MqttClientOptions _options;
    private readonly ILogger<MqttClientService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    /// <summary>
    /// Diccionario concurrente de manejadores para subscripciones a tópicos MQTT.
    /// </summary>
    private readonly ConcurrentDictionary<string, MessageDeserializatorHandler> _handlers = new();

    public MqttClientService(
        IOptions<MqttSettings> settings,
        IdGenerationService idGenerator,
        RequestCorrelationService correlationService,
        ILogger<MqttClientService> logger
    )
    {
        _logger = logger;
        _idGenerator = idGenerator;
        _correlationService = correlationService;

        var factory = new MqttClientFactory();
        _client = factory.CreateMqttClient();
        _options = new MqttClientOptionsBuilder()
            .WithTcpServer(settings.Value?.Host ?? "localhost", settings.Value?.Port)
            .WithClientId($"asp-server-{Guid.NewGuid()}")
            .WithCleanSession(false)
            .WithCredentials(settings.Value?.User ?? "", settings.Value?.Password ?? "")
            .Build();

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        _client.ApplicationMessageReceivedAsync += OnMessageReceived;
    }

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        _client.DisconnectedAsync += OnDisconnectAsync;
        await _client.ConnectAsync(_options, cancellationToken);
        _logger.LogInformation("Connected to MQTT broker!");
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        _client.DisconnectedAsync -= OnDisconnectAsync;
        await _client.DisconnectAsync(cancellationToken: cancellationToken);
        _logger.LogInformation("Disconnected from MQTT broker!");
    }

    /// <summary>
    /// Publica una solicitud y espera a que sea completada.
    /// </summary>
    /// <param name="request">Solicitud</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task PublishAsync(IStationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await PublishAsyncInternal(request, cancellationToken);

        // Verifica si la respuesta a la solicitud es satisfactoria
        if (response.ErrorCode != ErrorCode.Success)
            throw new Exception($"Failed to process the request: {response.ErrorCode}");
    }

    /// <summary>
    /// Publica una solicitud y espera a que sea completada para devolver un resultado.
    /// </summary>
    /// <typeparam name="TResult">Tipo correspondiente al resultado esperado</typeparam>
    /// <param name="request">Solicitud</param>
    /// <param name="cancellationToken">Token de cancelación</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<TResult> PublishAsync<TResult>(IStationRequest<TResult> request, CancellationToken cancellationToken = default)
    {
        var response = await PublishAsyncInternal(request, cancellationToken);

        // Verifica si la respuesta a la solicitud es satisfactoria
        if (response.ErrorCode != ErrorCode.Success)
            throw new Exception($"Failed to process the request: {response.ErrorCode}");

        // Verifica que exista un resultado en la respuesta
        TResult? result = ((IStationResponse<TResult>)response).Result ??
            throw new Exception("Expected a result");

        return result;
    }

    public async Task SubscribeAsync<TResult>(string topic, Func<IStationMessage, Task> handler)
    {
        _handlers[topic] = new()
        {
            Type = typeof(TResult),
            Handler = handler
        };

        await _client.SubscribeAsync(new MqttTopicFilterBuilder()
            .WithTopic(topic)
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build());
    }

    private async Task<IStationResponse> PublishAsyncInternal(IStationRequest request, CancellationToken cancellationToken = default)
    {
        int correlationId = _idGenerator.Next();
        request.Id = correlationId;

        var timeout = request.Timeout == default ?
            TimeSpan.FromSeconds(5) :
            request.Timeout;

        var payload = JsonSerializer.Serialize(request, _jsonOptions);
        var message = new MqttApplicationMessageBuilder()
            .WithTopic($"snapguard/v1/station/{request.DeviceId}/{request.Topic}")
            .WithPayload(payload)
            .WithQualityOfServiceLevel(request.QoSLevel)
            .Build();

        await _client.PublishAsync(message, cancellationToken);

        var response = await _correlationService.WaitForResponse(
            correlationId,
            timeout,
            cancellationToken
        );

        return response;
    }

    private async Task OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
    {
        var topic = e.ApplicationMessage.Topic;
        var payload = e.ApplicationMessage.ConvertPayloadToString();

        foreach (var (pattern, deserializationInfo) in _handlers)
        {
            if (MqttTopicFilterComparer.Compare(topic, pattern)
                == MqttTopicFilterCompareResult.IsMatch)
            {
                try
                {
                    var message = (IStationMessage?)JsonSerializer.Deserialize(
                        payload, deserializationInfo.Type, _jsonOptions);

                    if (message is not null)
                        await deserializationInfo.Handler(message);
                    else
                        _logger.LogWarning("Device message is empty");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Failed to deserialize device message: {message}", ex.Message);
                }

                return;
            }
        }
    }

    private async Task OnDisconnectAsync(MqttClientDisconnectedEventArgs e)
    {
         _logger.LogWarning("Trying to reconnect to MQTT broker...");
        await Task.Delay(TimeSpan.FromSeconds(5));

        try
        {
            await _client.ConnectAsync(_options);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to reconnect to MQTT broker");
        }
    }
}
