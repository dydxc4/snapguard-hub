namespace SnapGuard.Hub.Types.Alerts;

public class CameraStatusAlert
{
    public required int PicturesTaken { get; set; }

    public required bool IsFlashLedOn { get; set; }
}
