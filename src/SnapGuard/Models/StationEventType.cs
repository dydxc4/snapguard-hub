using SnapGuard.Models.Enums;

namespace SnapGuard.Models;

public record StationEventType
{
    public required string TypeCode { get; set; }

    public required string Description { get; set; }

    public StationEventSeverity Severity { get; set; }

    public ICollection<StationEvent> StationEvents { get; set; } = [];
}
