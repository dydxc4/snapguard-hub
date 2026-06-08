using SnapGuard.Hub.Types.Enums;

namespace SnapGuard.Hub.Types.Results;

public class CameraConfig
{
    public CameraFrameSize Resolution { get; set; }

    public CameraFrameFormat Format { get; set; }

    public int PanAngle { get; set; }

    public int TiltAngle { get; set; }

    public int Quality { get; set; }

    public int Brightness { get; set; }

    public int Contrast { get; set; }

    public int Saturation { get; set; }

    public int Sharpness { get; set; }

    public int SpecialEffect { get; set; }
}
