using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnapGuard.Hub.Requests;

public class SetCameraTimerConfigRequest() : StationRequestBase("camera/request/timer/set")
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Enabled { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnableFlash { get; set; }

    [Range(0, int.MaxValue)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Interval { get; set; }
}
