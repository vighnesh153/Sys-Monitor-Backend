namespace SystemMonitor.Models
{
    public class Wifi
    {
        public string Name { get; set; }
        public float SendSpeedInKBPS { get; set; }
        public float ReceiveSpeedInKBPS { get; set; }

        public string ConnectionType { get; set; }
        public string NetworkName { get; set; }
        public string IPv4_Address { get; set; }
        public string IPv6_Address { get; set; }

        public float SignalStrength { get; set; }
    }
}
