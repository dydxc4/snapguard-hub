namespace SnapGuard.Models;

public record UserRole
{
    public required string RoleCode { get; set; }

    public required string Name { get; set; }

    public bool CanModifyHub { get; set; }

    public bool CanQueryLogs { get; set; }

    public bool CanDeleteLogs { get; set; }

    public bool CanMakeRequests { get; set; }

    public bool CanCreateUsers { get; set; }

    public bool CanDeleteUsers { get; set; }

    public ICollection<User> Users { get; set; } = [];
}
