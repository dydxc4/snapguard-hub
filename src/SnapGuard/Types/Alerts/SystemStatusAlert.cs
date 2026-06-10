namespace SnapGuard.Types.Alerts;

public class SystemStatusAlert
{
    public int Rssi { get; set; }

    public long UpTime { get; set; }

    public int MaxHeapBlock { get; set; }

    public int MaxPSRAMBlock { get; set; }

    public int FreeHeap { get; set; }

    public int FreePSRAM { get; set; }
}
