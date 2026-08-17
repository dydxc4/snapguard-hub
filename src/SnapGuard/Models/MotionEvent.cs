namespace SnapGuard.Models;

public record MotionEvent
{
    public long MotionEventId { get; set; }

    public long StationId { get; set; }

    public Station Station { get; set; } = null!;

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset? EndedAt { get; set; }

    public int TriggerCount { get; set; }

    public ICollection<Picture> Pictures { get; set; } = [];
}
