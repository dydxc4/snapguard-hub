namespace SnapGuard.Hub.Helpers;

public static class ImageValidator
{
    private static readonly Dictionary<string, string> _mimeTypes = new()
    {
        { "image/jpeg", "jpg" },
        { "image/bmp", "bmp" }
    };

    private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
    {
        {
            "image/jpeg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xDB },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xEE }
            }
        },
        {
            "image/bmp", new List<byte[]>
            {
                "BM"u8.ToArray()
            }
        }
    };

    public static string? GetExtension(string mimeType)
    {
        _mimeTypes.TryGetValue(mimeType, out string? value);
        return value;
    }

    public static async Task<bool> Validate(string mimeType, Stream content)
    {
        if (content.Length == 0 || !content.CanRead | !content.CanSeek)
            return false;

        _fileSignatures.TryGetValue(mimeType, out var signatures);
        if (signatures is null) return false;

        int length = signatures.Max(s => s.Length);

        content.Seek(0, SeekOrigin.Begin);
        using var reader = new BinaryReader(content);
        var header = reader.ReadBytes(length);

        return signatures.Any(s => header.Take(s.Length).SequenceEqual(s));
    }
}
