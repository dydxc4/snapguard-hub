using System.Text.Json;
using MQTTnet;
using SnapGuard.Types.Alerts;

namespace SnapGuard.MockStation;

public class MqttStationService
{
    private readonly DateTimeOffset _startingTimestamp = DateTimeOffset.UtcNow;
    private readonly MqttStationOptions _stationOptions;
    private readonly JsonSerializerOptions _serializerOptions;

    private bool IsFlashOn { get; set; }

    private bool IsStreaming { get; set; }

    private int PicturesTaken { get; set; }

    public MqttStationService(MqttStationOptions options)
    {
        _stationOptions = options;
        _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
    }

    public async Task Start(MqttStationOptions clientConfig, CancellationToken cancellationToken = default)
    {
        // Establece las opciones de conexión
        var factory = new MqttClientFactory();
        var mqttClientOptionsBuiler = factory.CreateClientOptionsBuilder()
            .WithTcpServer(clientConfig.Host, clientConfig.Port)
            .WithClientId(clientConfig.StationId)
            .WithCleanSession(false);

        if (clientConfig.User is not null)
            mqttClientOptionsBuiler.WithCredentials(
                clientConfig.User,
                clientConfig.Password ?? "");

        var mqttClientOptions = mqttClientOptionsBuiler.Build();

        // Inicializa cliente MQTT
        using var mqttClient = factory.CreateMqttClient();
        mqttClient.ApplicationMessageReceivedAsync += OnMessageReceivedAsync;
        await mqttClient.ConnectAsync(mqttClientOptions, cancellationToken);

        string baseTopic = $"snapguard/{Constants.SNAPGUARD_VERSION}/station/{clientConfig.StationId}";

        // Realiza subscripción a los topics
        await mqttClient.SubscribeAsync($"{baseTopic}/+/config/get",
            cancellationToken: cancellationToken);
        await mqttClient.SubscribeAsync($"{baseTopic}/+/config/set",
            cancellationToken: cancellationToken);
        await mqttClient.SubscribeAsync($"{baseTopic}/+/request/+",
            cancellationToken: cancellationToken);

        // Envía un mensaje de arranque
        await mqttClient.PublishAsync($"{baseTopic}/system/alerts/startup",
            new StartupAlert { },
            jsonSerializerOptions: _serializerOptions,
            cancellationToken: cancellationToken);

        // Verifica si no se ha cancelado la tarea
        while (!cancellationToken.IsCancellationRequested)
        {
            // Actualiza estado de la estación
            await SendStatus(mqttClient, cancellationToken);
            ConsoleLogger.LogInfo("Status updated");

            // Echa a la suerte si deberá lanzar un evento
            if (Random.Shared.Next(4) == 0)
            {
                // Obtiene una cantidad de segundos de espera
                int secondsLeft = Random.Shared.Next(60);
                await Task.Delay(1000 * secondsLeft, cancellationToken);
                await SendEvent(mqttClient, cancellationToken);
                ConsoleLogger.LogInfo("Event raised");

                // Espera el tiempo restante
                await Task.Delay(1000 * (60 - secondsLeft), cancellationToken);
            }
            else
            {
                // Espera un minuto antes de la siguiente actualización
                await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);
            }
        }

        mqttClient.ApplicationMessageReceivedAsync -= OnMessageReceivedAsync;
        await mqttClient.DisconnectAsync(MqttClientDisconnectOptionsReason.NormalDisconnection, cancellationToken: default);
    }

    private async Task SendStatus(IMqttClient client, CancellationToken cancellationToken)
    {
        string baseTopic = $"snapguard/{Constants.SNAPGUARD_VERSION}/station/{_stationOptions.StationId}";
        var rng = Random.Shared;

        // Publica el estado del sistema
        SystemStatusAlert systemStatus = new()
        {
            Rssi = rng.Next(-80, -50),
            FreeHeap = 150000 - rng.Next(10000),
            FreePSRAM = 3000000 - rng.Next(20000),
            MaxHeapBlock = 100000 - rng.Next(5000),
            MaxPSRAMBlock = 500000 - rng.Next(5000),
            UpTime = (long)(DateTimeOffset.UtcNow - _startingTimestamp).TotalMilliseconds
        };

        await client.PublishAsync(
            $"{baseTopic}/system/status",
            systemStatus,
            jsonSerializerOptions: _serializerOptions,
            cancellationToken: cancellationToken);

        // Publica el estado del servicio de cámara
        CameraStatusAlert cameraStatus = new()
        {
            IsFlashLedOn = IsFlashOn,
            PicturesTaken = PicturesTaken
        };

        await client.PublishAsync(
            $"{baseTopic}/camera/status",
            cameraStatus,
            jsonSerializerOptions: _serializerOptions,
            cancellationToken: cancellationToken);

        // Publica el estado del servicio de streaming
        StreamingStatusAlert streamingStatus = new()
        {
            IsStreaming = IsStreaming,
            AverageFPS = IsStreaming ? 25 + 5 * rng.NextSingle() : 0,
            FreeStackSize = 2000 - rng.Next(750)
        };

        await client.PublishAsync(
            $"{baseTopic}/streaming/status",
            streamingStatus,
            jsonSerializerOptions: _serializerOptions,
            cancellationToken: cancellationToken);

        // Publica el estado del servicio de energía
        EnergyStatusAlert energyStatus = new()
        {
            BatteryVoltage = 3.7f + rng.NextSingle() / 10f,
            SolarPanelVoltage = 5 + rng.NextSingle() / 10f,
            BatteryLevel = 100
        };

        await client.PublishAsync(
            $"{baseTopic}/energy/status",
            energyStatus,
            jsonSerializerOptions: _serializerOptions,
            cancellationToken: cancellationToken);
    }

    private async Task SendEvent(IMqttClient client, CancellationToken cancellationToken)
    {

    }

    private async Task OnMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
    {
        Console.WriteLine("Message received on {0} from {1}",
            args.ApplicationMessage.Topic,
            args.ClientId);
    }
}
