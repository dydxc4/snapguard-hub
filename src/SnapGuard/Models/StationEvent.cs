using SnapGuard.Enums;

namespace SnapGuard.Models;

public record StationEvent
{
    public long EventId { get; set; }

    public long StationId { get; set; }

    public Station Station { get; set; } = null!;

    public EventType Type { get; set; }

    public DateTimeOffset RegisteredAt { get; set; }
}
