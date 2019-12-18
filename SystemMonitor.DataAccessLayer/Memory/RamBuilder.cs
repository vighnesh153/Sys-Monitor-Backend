using System.Diagnostics;

namespace SystemMonitor.DataAccessLayer.Memory
{
    public class RamBuilder
    {
        private PerformanceCounter AvailableRam { get; set; }
        private PerformanceCounter CommittesRam { get; set; }

        public RamBuilder()
        {
            AvailableRam = new PerformanceCounter("Memory", "Available Bytes");
            CommittesRam = new PerformanceCounter("Memory", "Committed Bytes");
        }

        public Models.Memory Get()
        {
            var memory = new Models.Memory();

            memory.InUseInBytes = CommittesRam.NextValue();
            memory.AvailableInBytes = AvailableRam.NextValue();

            return memory;
        }
    }
}
