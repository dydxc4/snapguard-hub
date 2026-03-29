using System.Collections.Concurrent;
using SnapGuard.Hub.Types;

namespace SnapGuard.Hub.Services;

public class RequestCorrelationService
{
    private readonly ConcurrentDictionary<int, TaskCompletionSource<IStationResponse>> _pendingRequests = new();

    public Task<IStationResponse> WaitForResponse(
        int correlationId,
        TimeSpan timeout,
        CancellationToken cancellationToken = default
    )
    {
        var tcs = new TaskCompletionSource<IStationResponse>();
        _pendingRequests[correlationId] = tcs;

        _ = Task.Delay(timeout, cancellationToken).ContinueWith(_ =>
        {
            if (_pendingRequests.TryRemove(correlationId, out var t))
                t.TrySetException(new TimeoutException("Device did not respond"));
        }, cancellationToken);

        return tcs.Task;
    }

    public void Resolve(int correlationId, IStationResponse response)
    {
        if (_pendingRequests.TryRemove(correlationId, out var tcs))
            tcs.TrySetResult(response);
    }
}
