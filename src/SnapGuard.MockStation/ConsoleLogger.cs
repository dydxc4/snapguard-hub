namespace SnapGuard.MockStation;

public static class ConsoleLogger
{
    public static void LogInfo(string text, params object?[]? args)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("[INFO] ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(DateTimeOffset.UtcNow.ToString());
        Console.ResetColor();
        Console.WriteLine($" {text}", args);
    }

    public static void LogWarning(string text, params object?[]? args)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("[WARNING] ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(DateTimeOffset.UtcNow.ToString());
        Console.ResetColor();
        Console.WriteLine($" {text}", args);
    }

    public static void LogError(string text, params object?[]? args)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[ERROR] ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(DateTimeOffset.UtcNow.ToString());
        Console.ResetColor();
        Console.WriteLine($" {text}", args);
    }
}
