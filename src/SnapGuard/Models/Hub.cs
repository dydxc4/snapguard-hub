namespace SnapGuard.Models;

public record Hub
{
    public long HubId { get; set; }

    public required string Name { get; set; }

    public DateTimeOffset RegisteredAt { get; set; }

    public ICollection<Station> Stations { get; set; } = [];

    public ICollection<User> Users { get; set; } = [];

    public ICollection<HubUser> HubUsers { get; set; } = [];
}
