using System;
using System.Diagnostics;
using System.Management;

namespace SystemMonitor.DataAccessLayer.Cpu
{
    public class CpuBuilder
    {
        private CpuInfo CpuInfo { get; set; }
        private PerformanceCounter CpuPerformanceCounter { get; set; }
      
        private uint CurrentSpeed;
        private uint MaxSpeed;

        public CpuBuilder()
        {
            CpuInfo = new CpuInfo();
            CpuPerformanceCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        }

        public Models.Cpu Get()
        {
            var cpu = new Models.Cpu();
            UpdateCPUSpeed();

            cpu.Name = CpuInfo.Get("Name") as string;
            cpu.Utilization = GetCpuUsage();
            cpu.SpeedInGHz = CurrentSpeed / 1024;
            cpu.NumberOfProcesses = Process.GetProcesses().Length;
            cpu.BaseSpeedInGHz = MaxSpeed / 1024;
            cpu.NumberOfCores = int.Parse($"{CpuInfo.Get("NumberOfCores")}");
            cpu.NumberOfLogicalProcessors = Environment.ProcessorCount;
            cpu.L1CacheInKB = (int)(CpuCache.GetCacheSizes(CacheLevel.Level1));
            cpu.L2CacheInKB = (int)(CpuCache.GetCacheSizes(CacheLevel.Level2));
            cpu.L3CacheInKB = (int)(CpuCache.GetCacheSizes(CacheLevel.Level3));
            
            return cpu;
        }

        public int GetCpuUsage()
        {
            var searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                var usage = $"{obj["PercentProcessorTime"]}";

                return int.Parse(usage);
            }
            return 0;
        }

        public void UpdateCPUSpeed()
        {
            using (var managementObject = new ManagementObject("Win32_Processor.DeviceID='CPU0'"))
            {
                CurrentSpeed = (uint)(managementObject["CurrentClockSpeed"]);
                MaxSpeed = (uint)(managementObject["MaxClockSpeed"]);
            }
        }
    }
}
