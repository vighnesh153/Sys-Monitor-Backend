namespace SystemMonitor.Models
{
    public class Memory
    {
        public float Total { get; set; }
        public float InUse { get; set; }
        public float Available 
        {
            get 
            {
                return Total - InUse;
            }
        }
    }
}
