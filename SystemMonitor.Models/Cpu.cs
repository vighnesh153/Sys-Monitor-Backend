namespace SystemMonitor.Models
{
    public class Cpu
    {
        public string Name { get; set; }
        public float Utilization { get; set; }
        public float SpeedInGHz { get; set; }
        public float NumberOfProcesses { get; set; }
        public float BaseSpeedInGHz { get; set; }
        public int NumberOfCores { get; set; }
        public int NumberOfLogicalProcessors { get; set; }
        public int L1CacheInKB { get; set; }
        public int L2CacheInKB { get; set; }
        public int L3CacheInKB { get; set; }
    }
}
