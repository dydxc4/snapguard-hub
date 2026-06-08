using SnapGuard.Hub.Types.Results;

namespace SnapGuard.Hub.Requests;

public class GetCameraConfigRequest() : StationRequestBase<CameraConfig>("camera/config/get");
