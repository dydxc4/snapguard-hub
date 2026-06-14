namespace SnapGuard.Models;

public record Hub
{
    public int HubId { get; set; }

    public required string Name { get; set; }

    public DateTimeOffset RegisteredAt { get; set; }

    public ICollection<Station> Stations { get; set; } = [];

    public ICollection<User> Users { get; set; } = [];
}
