using SnapGuard.Enums;

namespace SnapGuard.Types.Results;

public class CameraConfig
{
    public PictureResolution Resolution { get; set; }

    public PictureFormat Format { get; set; }

    public int PanAngle { get; set; }

    public int TiltAngle { get; set; }

    public int Quality { get; set; }

    public int Brightness { get; set; }

    public int Contrast { get; set; }

    public int Saturation { get; set; }

    public int Sharpness { get; set; }

    public int SpecialEffect { get; set; }
}
