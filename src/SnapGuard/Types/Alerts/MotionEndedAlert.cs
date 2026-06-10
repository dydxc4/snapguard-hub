namespace SnapGuard.Types.Alerts;

public class MotionEndedAlert : StationAlertBase
{
    public long TotalDuration { get; set; }

    public long StartTimestamp { get; set; }

    public long EndTimestamp { get; set; }
}
