namespace SnapGuard.Models;

public record MotionEvent
{
    public int MotionEventId { get; set; }

    public int StationId { get; set; }

    public Station Station { get; set; } = null!;

    public DateTimeOffset StartTimestamp { get; set; }

    public DateTimeOffset? EndTimestamp { get; set; }

    public int TriggerCount { get; set; }

    public ICollection<Picture> Pictures { get; set; } = [];
}
