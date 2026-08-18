using SnapGuard.Enums;

namespace SnapGuard.Models;

public class StationStreaming
{
    public int StreamingId { get; set; }

    public long StationId { get; set; }

    public Station Station { get; set; } = null!;

    public StreamingStatus Status { get; set; }

    public bool IsRecording { get; set; }

    public PictureResolution Resolution { get; set; }

    public string? Url { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset? FinishedAt { get; set; }
}
