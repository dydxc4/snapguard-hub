using SnapGuard.Types.Enums;

namespace SnapGuard.Types;

public class StationResponse : IStationResponse
{
    public required int RequestId { get; init; }

    public required ErrorCode ErrorCode { get; init; }

    public required long Timestamp { get; set; }
}

public class StationResponse<TResult> : StationResponse, IStationResponse<TResult>
{
    public TResult? Result { get; init; }
}
