using SnapGuard.Types;

namespace SnapGuard.Hub.Types;

public class MessageDeserializatorHandler
{
    public required Type Type { get; init; }

    public required Func<IStationMessage, Task> Handler { get; init; }
}
