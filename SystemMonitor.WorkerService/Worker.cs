using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SystemMonitor.ServiceLayer;

namespace SystemMonitor.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WorkerOptions options;

        public Worker(ILogger<Worker> logger, WorkerOptions options)
        {
            _logger = logger;
            this.options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var baseUrl = options.BaseUrl
                .Replace("<FIREBASE_PROJECT_ID>", options.FirebaseProjectId);

            while (!stoppingToken.IsCancellationRequested)
            {
                await new Transmitter(
                    baseUrl, options.CpuRoute, options.MemoryRoute, options.WifiRoute
                ).Transmit();

                _logger.LogInformation(Util.GetLogInformation());

                await Task.Delay(1, stoppingToken);
            }
        }
    }
}
