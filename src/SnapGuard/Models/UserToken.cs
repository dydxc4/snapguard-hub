namespace SnapGuard.Models;

public record UserToken
{
    public int TokenId { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTimeOffset ExpiresOn { get; set; }

    public string UserAgent { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }
}
