namespace SnapGuard.Helpers;

internal static class SnapGuardExtensions
{
    public static string GetMySqlEnumString<T>() where T : struct, Enum
    {
        var names = Enum.GetNames<T>().Select(x => $"'{x}'");
        string result = string.Join(", ", names);
        return $"ENUM({result})";
    }
}
