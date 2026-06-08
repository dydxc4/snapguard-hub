using SnapGuard.Hub.Types.Results;

namespace SnapGuard.Hub.Requests;

public class GetStreamingConfigRequest() : StationRequestBase<StreamingConfig>("streaming/config/get");
