namespace SnapGuard.Models;

public record User
{
    public int UserId { get; set; }

    public required string UserName { get; set; }

    public required string DisplayName { get; set; }

    public string Password { get; set; } = null!;

    public int HubId { get; set; }

    public Hub Hub { get; set; } = null!;

    public string RoleCode { get; set; } = null!;

    public UserRole Role { get; set; } = null!;

    public DateTimeOffset RegisteredAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<UserToken> UserTokens { get; set; } = [];
}
