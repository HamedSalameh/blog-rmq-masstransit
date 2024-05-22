using MassTransit;

namespace AuditServiceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger;
            this._bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                // await _bus.Publish(new AuditEvent(DateTime.Now, "Audit event"), stoppingToken);

                // Do some work here for auditing events
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
