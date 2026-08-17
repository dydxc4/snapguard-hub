using SnapGuard.Enums;

namespace SnapGuard.Models;

public record Picture
{
    public long PictureId { get; set; }

    public long? StationId { get; set; }

    public Station? Station { get; set; }

    public required string FileName { get; set; }

    public PictureFormat Format { get; set; }

    public PictureResolution Resolution { get; set; }

    public long? MotionEventId { get; set; }

    public MotionEvent? MotionEvent { get; set; }

    public DateTimeOffset UploadedAt { get; set; }
}
