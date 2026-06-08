using System.Text.Json.Serialization;
using SnapGuard.Hub.Types.Enums;

namespace SnapGuard.Hub.Requests;

public class SetStreamingConfigRequest() : StationRequestBase("streaming/config/set")
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public StreamingFrameRate? FrameRate { get; set; }
}
