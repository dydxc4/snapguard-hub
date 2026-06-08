using SnapGuard.Hub.Requests;
using SnapGuard.Hub.Types;

namespace SnapGuard.Hub.Services;

public interface IMqttClientService
{
    Task PublishAsync(IStationRequest request, CancellationToken cancellation = default);

    Task<TResult> PublishAsync<TResult>(IStationRequest<TResult> request, CancellationToken cancellation = default);

    Task SubscribeAsync<TResult>(string topic, Func<IStationMessage, Task> handler);
}
