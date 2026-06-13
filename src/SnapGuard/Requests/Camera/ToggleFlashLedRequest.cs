namespace SnapGuard.Requests;

public class ToggleFlashLedRequest() : StationRequestBase("camera/request/toggle_flash_led")
{
    public required bool IsOn { get; set; }
}
