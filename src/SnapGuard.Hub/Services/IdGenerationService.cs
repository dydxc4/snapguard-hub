namespace SnapGuard.Hub.Services;

public class IdGenerationService
{
    private int counter = 1;

    public int Next()
    {
        return counter++;
        //return Random.Shared.Next();
    }
}
