namespace SnapGuard.Models;

public record CameraModel
{
    public required string ModelCode { get; set; }

    public required string Name { get; set; }

    public bool SupportsJpeg { get; set; }

    public ICollection<Station> Stations { get; set; } = [];
}
