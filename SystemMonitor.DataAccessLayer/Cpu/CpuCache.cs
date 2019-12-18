using System;
using System.Collections.Generic;
using System.Text;

namespace SystemMonitor.DataAccessLayer.Cpu
{
    public enum CacheLevel : ushort
    {
        Level1 = 3,
        Level2 = 4,
        Level3 = 5,
    }

    public static class CpuCache
    {
        public static uint GetCacheSizes(CacheLevel level)
        {
            using (var managementClass = new ManagementClass("Win32_CacheMemory"))
            {
                var managementObjectCollection = managementClass.GetInstances();
                List<uint> cacheSizes = new List<uint>(managementObjectCollection.Count);

                cacheSizes.AddRange(managementObjectCollection
                  .Cast<ManagementObject>()
                  .Where(p => (ushort)(p.Properties["Level"].Value) == (ushort)level)
                  .Select(p => (uint)(p.Properties["MaxCacheSize"].Value)));

                uint sum = 0;
                cacheSizes.ForEach(item => sum += item);

                return sum;
            }
        }
    }
}
