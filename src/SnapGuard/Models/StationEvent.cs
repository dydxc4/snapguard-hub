namespace SnapGuard.Models;

public record StationEvent
{
    public int EventId { get; set; }

    public int StationId { get; set; }

    public Station Station { get; set; } = null!;

    public string TypeCode { get; set; } = null!;

    public StationEventType Type { get; set; } = null!;

    public DateTimeOffset RegisteredAt { get; set; }
}
