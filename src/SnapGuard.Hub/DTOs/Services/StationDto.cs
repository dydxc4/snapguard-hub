using SnapGuard.Enums;

namespace SnapGuard.Hub.DTOs.Services;

public class StationDto
{
    public long Id { get; set; }

    public required string Label { get; set; }

    public required string ModelName { get; set; }

    public StationStatus Status { get; set; }

    public bool HasMotionSensor { get; set; }
}
