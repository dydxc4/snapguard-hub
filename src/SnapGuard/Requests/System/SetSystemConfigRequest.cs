using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnapGuard.Requests;

public class SetSystemConfigRequest() : StationRequestBase("system/config/set")
{
    [Range(0, 600)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? UpdateStatusInterval { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnablePerformanceStatus { get; set; }
}
