using Newtonsoft.Json;

using SystemMonitor.DataAccessLayer.Cpu;
using SystemMonitor.DataAccessLayer.Memory;
using SystemMonitor.DataAccessLayer.Wifi;

namespace SystemMonitor.ServiceLayer
{
    public class Transmitter
    {
        public Transmitter(string baseUrl, string cpuRoute, string memoryRoute, string wifiRoute)
        {
            SendCpuStats(baseUrl + cpuRoute);
            SendMemoryStats(baseUrl + memoryRoute);
            SendWifiStats(baseUrl + wifiRoute);
        }

        private static void SendCpuStats(string url)
        {
            var cpu = new CpuBuilder().Get();
            var json = JsonConvert.SerializeObject(cpu, Formatting.Indented);

            RequestMaker.MakePutRequest(json, url);
        }

        private static void SendMemoryStats(string url)
        {
            var memory = new RamBuilder().Get();
            var json = JsonConvert.SerializeObject(memory, Formatting.Indented);

            RequestMaker.MakePutRequest(json, url);
        }

        private static void SendWifiStats(string url)
        {
            var wifi = new WifiBuilder().Get();
            var json = JsonConvert.SerializeObject(wifi, Formatting.Indented);

            RequestMaker.MakePutRequest(json, url);
        }
    }
}
