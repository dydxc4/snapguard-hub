using SnapGuard.Types.Enums;

namespace SnapGuard.Types;

public interface IStationResponse : IStationMessage
{
    int RequestId { get; init; }

    ErrorCode ErrorCode { get; init; }
}

public interface IStationResponse<TResult> : IStationResponse
{
    TResult? Result { get; init; }
}
