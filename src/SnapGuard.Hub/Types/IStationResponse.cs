using SnapGuard.Hub.Types.Enums;

namespace SnapGuard.Hub.Types;

public interface IStationResponse : IStationMessage
{
    int RequestId { get; init; }

    ErrorCode ErrorCode { get; init; }
}

public interface IStationResponse<TResult> : IStationResponse
{
    TResult? Result { get; init; }
}
