namespace SnapGuard.MockStation;

public static class ConsoleLogger
{
    public static void LogInfo(string text, params object?[]? args)
    {
        Log("INFO", ConsoleColor.Blue, text, args);
    }

    public static void LogWarning(string text, params object?[]? args)
    {
        Log("WARNING", ConsoleColor.Yellow, text, args);
    }

    public static void LogError(string text, params object?[]? args)
    {
        Log("ERROR", ConsoleColor.Red, text, args);
    }

    private static void Log(string tag, ConsoleColor tagColor, string text, params object?[]? args)
    {
        Console.ForegroundColor = tagColor;
        Console.Write($"[{tag}] ");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(DateTime.UtcNow.ToString());
        Console.ResetColor();
        Console.WriteLine($" {text}", args);
    }
}
