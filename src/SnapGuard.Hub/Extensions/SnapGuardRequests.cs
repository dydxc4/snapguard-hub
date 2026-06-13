using SnapGuard.Requests;
using SnapGuard.Hub.Services;
using SnapGuard.Types.Results;
using SnapGuard.Types.Enums;

namespace SnapGuard.Hub.Extensions;

public static class SnapGuardRequests
{
    extension(IMqttClientService clientService)
    {
        #region Camera requests

        public async Task<CameraConfig> GetCameraConfig(
            int deviceId, CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new GetCameraConfigRequest { DeviceId = deviceId },
            cancellationToken
        );

        public async Task SetCameraConfig(
            int deviceId,
            CameraFrameSize? resolution = null,
            CameraFrameFormat? format = null,
            decimal? panAngle = null,
            decimal? tiltAngle = null,
            int? quality = null,
            int? brightness = null,
            int? contrast = null,
            int? saturation = null,
            int? sharpness = null,
            int? specialEffect = null,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new SetCameraConfigRequest
            {
                DeviceId = deviceId,
                Resolution = resolution,
                Format = format,
                PanAngle = panAngle,
                TiltAngle = tiltAngle,
                Quality = quality,
                Brightness = brightness,
                Contrast = contrast,
                Saturation = saturation,
                Sharpness = sharpness,
                SpecialEffect = specialEffect,
            },
            cancellationToken
        );

        public async Task ToggleFlashLed(
            int deviceId,
            bool isOn,
            CancellationToken cancellationToken = default
        ) => await clientService.PublishAsync(
            new ToggleFlashLedRequest
            {
                DeviceId = deviceId,
                IsOn = isOn
            },
            cancellationToken
        );

        public async Task TakePicture(
            int deviceId,
            bool? enableFlash = null,
            CancellationToken cancellationToken = default
        ) => await clientService.PublishAsync(
            new TakePictureRequest
            {
                DeviceId = deviceId,
                EnableFlash = enableFlash
            },
            cancellationToken
        );

        public async Task<CameraTimerConfig> GetCameraTimerConfig(
            int deviceId,
            CancellationToken cancellationToken = default
        ) => await clientService.PublishAsync(
            new GetCameraTimerConfigRequest { DeviceId = deviceId },
            cancellationToken
        );

        public async Task SetCameraTimerConfig(
            int deviceId,
            bool? enabled = null,
            bool? enableFlash = null,
            int? interval = null,
            CancellationToken cancellationToken = default
        ) => await clientService.PublishAsync(
            new SetCameraTimerConfigRequest
            {
                DeviceId = deviceId,
                Enabled = enabled,
                EnableFlash = enableFlash,
                Interval = interval,
            },
            cancellationToken
        );

        #endregion

        #region Motion sensor requests

        public async Task<MotionSensorConfig> GetMotionSensorConfig(
            int deviceId,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new GetMotionConfigRequest { DeviceId = deviceId },
            cancellationToken
        );

        public async Task SetMotionSensorConfig(
            int deviceId,
            bool? enabled,
            int? detectionThreshold,
            int? endDetectionWaitTimeout,
            bool? enablePictureTaking,
            bool? enableFlash,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new SetMotionConfigRequest
            {
                DeviceId = deviceId,
                Enabled = enabled,
                DetectionThreshold = detectionThreshold,
                EndDetectionWaitTimeout = endDetectionWaitTimeout,
                EnablePictureTaking = enablePictureTaking,
                EnableFlash = enableFlash,
            },
            cancellationToken
        );

        #endregion

        #region Streaming requests

        public async Task<StreamingConfig> GetStreamingConfig(
            int deviceId,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new GetStreamingConfigRequest { DeviceId = deviceId },
            cancellationToken
        );

        public async Task SetStreamingConfig(
            int deviceId,
            StreamingFrameRate? frameRate = null,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new SetStreamingConfigRequest
            {
                DeviceId = deviceId,
                FrameRate = frameRate
            },
            cancellationToken
        );

        public async Task StartStreaming(
            int deviceId,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new StartStreamingRequest
            {
                DeviceId = deviceId,
            },
            cancellationToken
        );

        public async Task StopStreaming(
            int deviceId,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new StopStreamingRequest
            {
                DeviceId = deviceId,
            },
            cancellationToken
        );

        #endregion

        #region System requests

        public async Task<SystemConfig> GetSystemConfig(
            int deviceId,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new GetSystemConfigRequest { DeviceId = deviceId },
            cancellationToken
        );

        public async Task SetSystemConfig(
            int deviceId,
            int? updateStatusInterval = null,
            bool? enablePerformanceStatus = null,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new SetSystemConfigRequest
            {
                DeviceId = deviceId,
                UpdateStatusInterval = updateStatusInterval,
                EnablePerformanceStatus = enablePerformanceStatus
            },
            cancellationToken
        );

        public async Task Restart(
            int deviceId,
            CancellationToken cancellationToken = default
        ) =>  await clientService.PublishAsync(
            new RestartRequest { DeviceId = deviceId },
            cancellationToken
        );

        #endregion
    }
}
