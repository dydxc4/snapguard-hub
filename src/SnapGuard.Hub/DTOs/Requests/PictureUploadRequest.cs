using Microsoft.AspNetCore.Mvc;
using SnapGuard.Types.Enums;

namespace SnapGuard.Hub.DTOs.Requests;

public class PictureUploadRequest
{
    [FromForm(Name = "picture")]
    public required IFormFile Picture { get; init; }

    [FromHeader(Name = "SG-Resolution")]
    public required CameraFrameSize Resolution { get; init; }

    [FromHeader(Name = "SG-Timestamp")]
    public required long Timestamp { get; init; }

    [FromHeader(Name = "SG-Motion-Timestamp")]
    public long? MotionTimestamp { get; init; }
}
