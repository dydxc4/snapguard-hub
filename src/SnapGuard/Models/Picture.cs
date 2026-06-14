namespace SnapGuard.Models;

public record Picture
{
    public int PictureId { get; set; }

    public int? StationId { get; set; }

    public Station? Station { get; set; }

    public required string FileName { get; set; }

    public CameraFrameFormat Format { get; set; }

    public CameraFrameSize Resolution { get; set; }

    public int? MotionEventId { get; set; }

    public MotionEvent? MotionEvent { get; set; }

    public DateTimeOffset UploadedAt { get; set; }
}
