using SnapGuard.Enums;

namespace SnapGuard.Models;

public class HubUser
{
    public long HubId { get; set; }

    public Hub Hub { get; set; } = null!;

    public long UserId { get; set; }

    public User User { get; set; } = null!;

    public DateTimeOffset JoinedAt { get; set; }

    public UserRole Role { get; set; }
}
