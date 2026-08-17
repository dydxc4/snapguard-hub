namespace SnapGuard.Models;

public record User
{
    public long UserId { get; set; }

    public required string Email { get; set; }

    public required string UserName { get; set; }

    public required string DisplayName { get; set; }

    public required string Password { get; set; }

    public bool IsActive { get; set; }

    public bool IsStaff { get; set; }

    public DateTimeOffset RegisteredAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public DateTimeOffset? LastLoggedAt { get; set; }

    public ICollection<OutstandingToken> OutstandingTokens { get; set; } = [];

    public ICollection<Hub> Hubs { get; set; } = [];

    public ICollection<HubUser> HubUsers { get; set; } = [];

    public ICollection<UserNotification> Notifications { get; set; } = [];
}
