using SnapGuard.Hub.Types;
using SnapGuard.Hub.Types.Results;

namespace SnapGuard.Hub.Services;

public class MqttBackgroundService(
    ILogger<MqttBackgroundService> logger,
    IMqttClientService mqttClient,
    RequestCorrelationService correlationService
) : IHostedService
{
    private readonly ILogger<MqttBackgroundService> _logger = logger;
    private readonly MqttClientService _mqttService = (MqttClientService)mqttClient;
    private readonly RequestCorrelationService _correlationService = correlationService;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mqttService.ConnectAsync(cancellationToken);

        // Realiza las suscripciones a los tópicos de respuesta sin objeto de resultado
        await _mqttService.SubscribeAsync<StationResponse>(
            "snapguard/v1/station/+/+/config/set/response",
            OnDeviceResponse
        );

        await _mqttService.SubscribeAsync<StationResponse>(
            "snapguard/v1/station/+/+/request/+/response",
            OnDeviceResponse
        );

        // Realiza las suscripciones a los tópicos de respuesta con objeto de resultado
        await _mqttService.SubscribeAsync<StationResponse<CameraConfig>>(
            "snapguard/v1/station/+/camera/config/get/response",
            OnDeviceResponse
        );
        await _mqttService.SubscribeAsync<StationResponse<CameraTimerConfig>>(
            "snapguard/v1/station/+/camera/timer/config/get/response",
            OnDeviceResponse
        );
        await _mqttService.SubscribeAsync<StationResponse<SystemConfig>>(
            "snapguard/v1/station/+/system/config/get/response",
            OnDeviceResponse
        );
        await _mqttService.SubscribeAsync<StationResponse<StreamingConfig>>(
            "snapguard/v1/station/+/streaming/config/get/response",
            OnDeviceResponse
        );
        await _mqttService.SubscribeAsync<StationResponse<MotionSensorConfig>>(
            "snapguard/v1/station/+/motion/config/get/response",
            OnDeviceResponse
        );
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _mqttService.DisconnectAsync(cancellationToken);
    }

    private async Task OnDeviceResponse(IStationMessage message)
    {
        var response = (IStationResponse)message;
        _correlationService.Resolve(response.RequestId, response);
    }

    private async Task OnDeviceEvent(IStationMessage message)
    {

    }
}
