using Microsoft.AspNetCore.Mvc;
using SnapGuard.Hub.Types.Enums;

namespace SnapGuard.Hub.DTOs.Requests;

public class PictureUploadRequest
{
    [FromForm(Name = "picture")]
    public required IFormFile Picture { get; set; }

    [FromHeader(Name = "resolution")]
    public required CameraFrameSize Resolution { get; set; }

    [FromHeader(Name = "timestamp")]
    public required long Timestamp { get; set; }

    [FromHeader(Name = "motionTimestamp")]
    public long? MotionTimestamp { get; set; }
}
