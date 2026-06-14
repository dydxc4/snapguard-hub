using System.Net.NetworkInformation;

namespace SnapGuard.Models;

public record Station
{
    public int StationId { get; set; }

    public int HubId { get; set; }

    public Hub Hub { get; set; } = null!;

    public required string Name { get; set; }

    public PhysicalAddress MacAddress { get; set; } = null!;

    public bool IsSolarPowered { get; set; }

    public bool IsBatteryPowered { get; set; }

    public bool HasCameraFlash { get; set; }

    public bool HasPanTiltControl { get; set; }

    public bool HasNightVision { get; set; }

    public string Version { get; set; } = null!;

    public string CoreVersion { get; set; } = null!;

    public string CameraModelCode { get; set; } = null!;

    public CameraModel CameraModel { get; set; } = null!;

    public DateTimeOffset RegisteredAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<Picture> Pictures { get; set; } = [];

    public ICollection<StationEvent> Events { get; set; } = [];

    public ICollection<MotionEvent> MotionEvents { get; set; } = [];

    public ICollection<StationToken> StationTokens { get; set; } = [];
}
