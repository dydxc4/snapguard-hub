namespace SnapGuard.Types.Alerts;

public class StreamingStatusAlert : StationAlertBase
{
    public int FreeStackSize { get; set; }

    public bool IsStreaming { get; set; }

    public float AverageFPS { get; set; }
}
