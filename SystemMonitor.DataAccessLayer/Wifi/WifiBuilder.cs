namespace SystemMonitor.DataAccessLayer.Wifi
{
    public class WifiBuilder
    {
        private WifiInfo wifiInfo;
        public WifiBuilder()
        {
            wifiInfo = new WifiInfo();
        }

        public Models.Wifi Get()
        {
            var wifi = new Models.Wifi();

            wifi.Name = WifiInfo.GetWirelessInstanceName();
            wifi.ReceiveSpeedInKBPS = ((float)WifiInfo.Get("Bytes Received/sec")) / 1024;
            wifi.SendSpeedInKBPS = ((float)WifiInfo.Get("Bytes Sent/sec")) / 1024;

            wifi.NetworkName = WifiInfo.OtherProperties("SSID");

            wifi.SignalStrength = float.Parse(WifiInfo.OtherProperties("Signal"));
            wifi.ConnectionType = WifiInfo.OtherProperties("Radio type");

            wifi.IPv4_Address = WifiInfo.GetIPv4Address();
            wifi.IPv6_Address = WifiInfo.GetIPv6Address();

            return wifi;
        }
    }
}
