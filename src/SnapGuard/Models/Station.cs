using System.Net.NetworkInformation;
using SnapGuard.Enums;

namespace SnapGuard.Models;

public record Station
{
    public long StationId { get; set; }

    public long HubId { get; set; }

    public Hub Hub { get; set; } = null!;

    public long StationModelId { get; set; }

    public StationModel StationModel { get; set; } = null!;

    public required string Label { get; set; }

    public PhysicalAddress MacAddress { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public StationStatus Status { get; set; }

    public string Version { get; set; } = null!;

    public string CoreVersion { get; set; } = null!;

    public DateTimeOffset RegisteredAt { get; set; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public ICollection<Picture> Pictures { get; set; } = [];

    public ICollection<StationEvent> Events { get; set; } = [];

    public ICollection<MotionEvent> MotionEvents { get; set; } = [];

    public ICollection<StationToken> StationTokens { get; set; } = [];

    public ICollection<LiveStream> Streamings { get; set; } = [];
}
