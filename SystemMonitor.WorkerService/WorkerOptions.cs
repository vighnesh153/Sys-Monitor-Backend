namespace SystemMonitor.WorkerService
{
    public class WorkerOptions
    {
        public string FirebaseProjectId { get; set; }
        public string BaseUrl { get; set; }
        public string CpuRoute { get; set; }
        public string MemoryRoute { get; set; }
        public string WifiRoute { get; set; }
    }
}
