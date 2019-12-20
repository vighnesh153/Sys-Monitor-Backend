using Newtonsoft.Json;
using System.Threading.Tasks;
using SystemMonitor.DataAccessLayer.Cpu;
using SystemMonitor.DataAccessLayer.Memory;
using SystemMonitor.DataAccessLayer.Wifi;

namespace SystemMonitor.ServiceLayer
{
    public class Transmitter
    {
        string _baseUrl;
        string _cpuRoute;
        string _memoryRoute;
        string _wifiRoute;

        public Transmitter(string baseUrl, string cpuRoute, string memoryRoute, string wifiRoute)
        {
            _baseUrl = baseUrl;
            _cpuRoute = cpuRoute;
            _memoryRoute = memoryRoute;
            _wifiRoute = wifiRoute;
        }

        public async Task<bool> Transmit()
        {
            await SendCpuStats(_baseUrl + _cpuRoute);
            await SendMemoryStats(_baseUrl + _memoryRoute);
            await SendWifiStats(_baseUrl + _wifiRoute);

            return true;
        }

        private static async Task<bool> SendCpuStats(string url)
        {
            var cpu = new CpuBuilder().Get();
            var json = JsonConvert.SerializeObject(cpu, Formatting.Indented);

            await RequestMaker.MakePutRequest(json, url);
            return true;
        }

        private static async Task<bool> SendMemoryStats(string url)
        {
            var memory = new RamBuilder().Get();
            var json = JsonConvert.SerializeObject(memory, Formatting.Indented);

            await RequestMaker.MakePutRequest(json, url);
            return true;
        }

        private static async Task<bool> SendWifiStats(string url)
        {
            var wifi = new WifiBuilder().Get();
            var json = JsonConvert.SerializeObject(wifi, Formatting.Indented);

            await RequestMaker.MakePutRequest(json, url);
            return true;
        }
    }
}
