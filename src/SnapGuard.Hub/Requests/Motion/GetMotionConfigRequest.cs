using SnapGuard.Hub.Types.Results;

namespace SnapGuard.Hub.Requests;

public class GetMotionConfigRequest() : StationRequestBase<MotionSensorConfig>("motion/config/get");
