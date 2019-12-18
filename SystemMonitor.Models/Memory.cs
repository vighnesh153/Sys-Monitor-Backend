namespace SystemMonitor.Models
{
    public class Memory
    {
        public float TotalInBytes
        {
            get
            {
                return AvailableInBytes + InUseInBytes;
            }
        }
        public float InUseInBytes { get; set; }
        public float AvailableInBytes { get; set; }
    }
}
