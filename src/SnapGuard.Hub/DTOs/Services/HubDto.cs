using SnapGuard.Enums;

namespace SnapGuard.Hub.DTOs.Services;

public class HubDto
{
    public long Id { get; set; }

    public required string Name { get; set; }

    public HubUserRole UserRole { get; set; }
}
