using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SnapGuard.Requests;

public class SetCameraConfigRequest() : StationRequestBase<StationResponse>("camera/config/set")
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CameraFrameSize? Resolution { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public CameraFrameFormat? Format { get; set; }

    [Range(-90, 90)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? PanAngle { get; set; }

    [Range(-90, 90)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public decimal? TiltAngle { get; set; }

    [Range(0, 63)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Quality { get; set; }

    [Range(-2, 2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Brightness { get; set; }

    [Range(-2, 2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Contrast { get; set; }

    [Range(-2, 2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Saturation { get; set; }

    [Range(-2, 2)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Sharpness { get; set; }

    [Range(0, 6)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SpecialEffect { get; set; }
}
