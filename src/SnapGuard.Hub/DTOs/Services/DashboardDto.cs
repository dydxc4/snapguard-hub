using SnapGuard.Hub.DTOs.Enums;

namespace SnapGuard.Hub.DTOs.Services;

public class DashboardDto
{
    public required string UserFirstName { get; set; }

    public required HubDto SelectedHub { get; set; }

    public List<HubDto> Hubs { get; set; } = [];

    public Dictionary<CounterType, EventCounterDto> Counters { get; set; } = [];

    public List<LiveStreamDto> LiveStreams { get; set; } = [];

    public List<StationDto> Devices { get; set; } = [];

    public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
}
