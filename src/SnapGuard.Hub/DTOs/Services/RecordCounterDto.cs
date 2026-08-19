namespace SnapGuard.Hub.DTOs.Services;

public class EventCounterDto
{
    public int Count { get; set; }

    public string? LastEventDescription { get; set; }

    public DateTimeOffset? LastEventTimestamp { get; set; }
}
