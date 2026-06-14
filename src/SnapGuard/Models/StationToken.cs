namespace SnapGuard.Models;

public record StationToken
{
    public int TokenId { get; set; }

    public int StationId { get; set; }

    public Station Station { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTimeOffset ExpiresOn { get; set; }
}
