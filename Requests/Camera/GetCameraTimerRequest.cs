using SnapGuard.Hub.Types.Results;

namespace SnapGuard.Hub.Requests;

public class GetCameraTimerConfigRequest() : StationRequestBase<CameraTimerConfig>("camera/timer/config/get");
