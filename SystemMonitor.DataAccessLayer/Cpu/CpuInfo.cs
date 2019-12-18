using System.Management;
using System.Collections.Generic;

namespace SystemMonitor.DataAccessLayer.Cpu
{
    public class CpuInfo
    {
        private Dictionary<string, object> _items;

        public CpuInfo()
        {
            _items = new Dictionary<string, object>();

            using (var managementClass = new ManagementClass("win32_processor"))
            {
                using (var managementObjectCollection = managementClass.GetInstances())
                {
                    foreach (var managementObject in managementObjectCollection)
                    {
                        foreach (var prop in managementObject.Properties)
                        {
                            _items[prop.Name] = prop.Value;
                        }
                    }
                }
            }
        }

        public object Get(string key) => _items[key];
    }
}
