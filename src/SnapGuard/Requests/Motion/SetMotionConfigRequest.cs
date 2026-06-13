using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnapGuard.Requests;

public class SetMotionConfigRequest() : StationRequestBase("motion/config/set")
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Enabled { get; set; }

    [Range(0, 60)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? DetectionThreshold { get; set; }

    [Range(0, 300)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EndDetectionWaitTimeout { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnablePictureTaking { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? EnableFlash { get; set; }
}
