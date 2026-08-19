using SnapGuard.Enums;
using SnapGuard.Hub.DTOs.Enums;

namespace SnapGuard.Hub.DTOs.Services;

public class LiveStreamDto
{
    public long Id { get; set; }

    public required string DeviceLabel { get; set; }

    public string? StreamingUrl { get; set; }

    public string? PreviewUrl { get; set; }

    public bool IsRecording { get; set; }

    public LiveStreamStatus Status { get; set; }

    public LiveStreamQuality Quality { get; set; }
}
