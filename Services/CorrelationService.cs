using System.Collections.Concurrent;

namespace SnapGuard.Hub.Services;

public class CorrelationService
{
    private readonly ConcurrentDictionary<int, string> _pendingCorrelationIds = new();

    public async Task RegisterAsync(int correlationId, string connectionId)
    {
        _pendingCorrelationIds[correlationId] = connectionId;
    }

    public async Task<string?> GetConnectionId(int correlationId)
    {
        if (_pendingCorrelationIds.TryRemove(correlationId, out string? connectionId))
            return connectionId;

        return null;
    }
}
