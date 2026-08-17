using SnapGuard.Enums;

namespace SnapGuard.Models;

public class StationModel
{
    public long StationModelId { get; set; }

    public required string Name { get; set; }

    public bool IsSolarPowered { get; set; }

    public bool IsBatteryPowered { get; set; }

    public bool HasCameraFlash { get; set; }

    public bool HasPanTiltControl { get; set; }

    public bool HasNightVision { get; set; }

    public CameraModel CameraModel { get; set; }

    public DateTimeOffset RegisteredAt { get; set; }

    public IList<Station> Stations { get; set; } = [];
}
