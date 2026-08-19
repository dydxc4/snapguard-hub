using SnapGuard.Hub.DTOs.Enums;
using SnapGuard.Hub.DTOs.Services;
using SnapGuard.Models;

namespace SnapGuard.Hub.Services;

public class DashboardService(SnapGuardContext dbContext, ILogger<DashboardService> logger)
{
    private readonly ILogger<DashboardService> _logger = logger;
    private readonly SnapGuardContext _dbContext = dbContext;

    public async Task<DashboardDto> GetDashboardAsync(long userId, long hubId)
    {
        return new()
        {
            UserFirstName = "Test user",
            SelectedHub = new()
            {
                Id = 1,
                Name = "Hub 1",
                UserRole = Enums.HubUserRole.OWNER
            },
            Counters =
            {
                { CounterType.PHOTO, new() { Count = 10, LastEventTimestamp = DateTimeOffset.Now } },
                { CounterType.EVENT, new() { Count = 5, LastEventTimestamp = DateTimeOffset.Now } },
                { CounterType.MOTION, new() },
            },
            Devices =
            {
                {
                    new()
                    {
                        Id = 1,
                        Label = "CAM1: Patio",
                        ModelName = "ESP32-CAM",
                        HasMotionSensor = true,
                    }
                },
                {
                    new()
                    {
                        Id = 2,
                        Label = "CAM2: Hall",
                        ModelName = "ESP32-CAM"
                    }
                },
            },
            LiveStreams =
            {
                { new()
                    {
                        Id = 1,
                        DeviceLabel = "CAM1: Patio",
                        Status = Enums.LiveStreamStatus.PLAYING,
                        Quality = LiveStreamQuality.FULL_HD,
                        IsRecording = true
                    }
                },
                { new()
                    {
                        Id = 2,
                        DeviceLabel = "CAM2: Hall",
                        Status = Enums.LiveStreamStatus.STOPPED,
                        Quality = LiveStreamQuality.QUAD_HD
                    }
                },
                { new()
                    {
                        Id = 3,
                        DeviceLabel = "CAM3: Entrance",
                        Status = Enums.LiveStreamStatus.SIGNAL_LOST,
                        Quality = LiveStreamQuality.FULL_ULTRA_HD
                    }
                },
            },
        };
    }
}
