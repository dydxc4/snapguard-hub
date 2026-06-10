namespace SnapGuard.Types.Alerts;

public class MotionDetectedAlert : StationAlertBase
{
    public int TriggerCount { get; set; }

    public long StartTimestamp { get; set; }

    public long TriggerTimestamp { get; set; }
}
