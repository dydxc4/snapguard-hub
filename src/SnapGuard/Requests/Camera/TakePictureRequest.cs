namespace SnapGuard.Requests;

public class TakePictureRequest() : StationRequestBase("camera/request/take_picture")
{
    public bool? EnableFlash { get; set; }
}
