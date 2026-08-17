namespace SnapGuard.Models;

public record OutstandingToken
{
    public long TokenId { get; set; }

    public required string Token { get; set; }

    public long? UserId { get; set; }

    public User? User { get; set; }

    public bool IsBlacklisted { get; set; }

    public DateTimeOffset CreateAt { get; set; }

    public DateTimeOffset ExpiresAt { get; set; }

    public DateTimeOffset? BlockedAt { get; set; }

    public required string Jti { get; set; }
}
