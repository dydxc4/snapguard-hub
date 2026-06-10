namespace SnapGuard.MockStation;

public class MqttStationOptions
{
    public required string Host { get; set; }

    public int? Port { get; set; }

    public required string StationId { get; set; }

    public string? User { get; set; }

    public string? Password { get; set; }

    public int? StatusUpdateInverval { get; set; }
}
