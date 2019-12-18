using System;
using System.Linq;
using System.Collections.Generic;

using System.Net;
using System.Diagnostics;

namespace SystemMonitor.DataAccessLayer.Wifi
{
    public class WifiInfo
    {
        private static Dictionary<string, string> _wirelessNetworkProperties;
        public static object Get(string key)
        {
            var networkPerformanceCounter = new PerformanceCounter(
                "Network Interface", key, GetWirelessInstanceName()
            );

            return networkPerformanceCounter.NextValue();
        }

        public static string GetWirelessInstanceName()
        {
            var categoryName = "Network Interface";
            var perfCounter = new PerformanceCounterCategory(categoryName);
            foreach (var instanceName in perfCounter.GetInstanceNames())
            {
                if (instanceName.ToLower().Contains("wireless") == true)
                {
                    return instanceName;
                }
            }
            return "";
        }

        public static string GetIPv4Address()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList
                    .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Last().ToString();
        }

        public static string GetIPv6Address()
        {
            return Dns.GetHostEntry(Dns.GetHostName()).AddressList
                    .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    .Last().ToString();
        }

        public static string OtherProperties(string key)
        {
            UpdateOtherProperties();
            return _wirelessNetworkProperties[key];
        }

        private static void UpdateOtherProperties()
        {
            var process = new Process();
            process.StartInfo.FileName = "netsh.exe";
            process.StartInfo.Arguments = "wlan show interfaces";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();

            var output = process.StandardOutput
                            .ReadToEnd()
                            .Split(Environment.NewLine)
                            .Skip(3)    // Remove first 3 and last 4 not-required lines
                            .SkipLast(4)
                            .Select(x => x.Trim().Split(":").Select(y => y.Trim()))
                            .ToList();

            process.Close();

            _wirelessNetworkProperties = new Dictionary<string, string>();

            foreach (var keyAndValue in output)
            {
                var keyValue = keyAndValue.ToList();
                _wirelessNetworkProperties[keyValue[0]] = _wirelessNetworkProperties[keyValue[1]];
            }
        }
    }
}
