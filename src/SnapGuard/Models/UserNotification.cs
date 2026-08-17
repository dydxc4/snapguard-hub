using SnapGuard.Enums;

namespace SnapGuard.Models;

public class UserNotification
{
    public long NotificationId { get; set; }

    public long UserId { get; set; }

    public User User { get; set; } = null!;

    public required string Title { get; set; }

    public string? Content { get; set; }

    public NotificationType Type { get; set; }

    public bool IsRead { get; set; }

    public DateTimeOffset ReceivedAt { get; set; }
}
