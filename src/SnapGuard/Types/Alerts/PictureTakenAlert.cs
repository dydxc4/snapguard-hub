using SnapGuard.Enums;

namespace SnapGuard.Types.Alerts;

public class PictureTakenAlert : StationAlertBase
{
    public int Id { get; set; }

    public int Size { get; set; }

    public long? MotionTimestamp { get; set; }

    public TakePictureRequestSource Source { get; set; }

    public PictureResolution Resolution { get; set; }

    public PictureFormat Format { get; set; }
}
