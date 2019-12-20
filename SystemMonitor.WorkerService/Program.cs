using Serilog;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SystemMonitor.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Logs\system_monitor_logs.txt")
                .CreateLogger();

            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddSerilog();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(GetDbInfoOptions(hostContext));
                    services.AddHostedService<Worker>();
                });
        }

        public static WorkerOptions GetDbInfoOptions(HostBuilderContext hostBuilderContext) => 
            hostBuilderContext
                .Configuration
                .GetSection("DbInfo")
                .Get<WorkerOptions>();
    }
}
