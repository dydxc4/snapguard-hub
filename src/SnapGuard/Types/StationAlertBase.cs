namespace SnapGuard.Types;

public abstract class StationAlertBase : IStationMessage
{
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}
