namespace SnapGuard.Models;

public record StationToken
{
    public long TokenId { get; set; }

    public long StationId { get; set; }

    public Station Station { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool IsBlocked { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }
}
