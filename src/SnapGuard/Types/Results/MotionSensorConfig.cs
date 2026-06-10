namespace SnapGuard.Types.Results;

public class MotionSensorConfig
{
    public bool Enabled { get; set; }

    public int DetectionThreshold { get; set; }

    public int EndDetectionWaitTimeout { get; set; }

    public bool EnablePictureTaking { get; set; }

    public bool EnableFlash { get; set; }
}
