namespace SnapGuard.Hub.Requests;

public class ToggleFlashLedRequest() : StationRequestBase("camera/request/toggle_flash_led")
{
    public required bool IsOn { get; set; }
}
