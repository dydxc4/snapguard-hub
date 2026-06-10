using SnapGuard.Types.Enums;

namespace SnapGuard.Types.Alerts;

public class CameraStatusAlert : StationAlertBase
{
    public int PicturesTaken { get; set; }

    public CameraTimerState TimerState { get; set; }

    public bool IsFlashLedOn { get; set; }
}
