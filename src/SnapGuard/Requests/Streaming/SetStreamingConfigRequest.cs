using System.Text.Json.Serialization;

namespace SnapGuard.Requests;

public class SetStreamingConfigRequest() : StationRequestBase("streaming/config/set")
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StreamingFrameRate? FrameRate { get; set; }
}
