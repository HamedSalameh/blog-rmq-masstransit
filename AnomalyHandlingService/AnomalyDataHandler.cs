using Common;

namespace AnomalyHandlingService
{
    public class AnomalyDataHandler : IAnomalyDataHandler
    {
        private readonly ILogger<AnomalyDataHandler> _logger;

        public AnomalyDataHandler(ILogger<AnomalyDataHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(Anomaly message)
        {
            // Do some work here to handle the anomaly
            _logger.LogInformation("Handling anomaly: {anomaly}", message.ToString());

            await Task.Delay(200);  // fake work ...

            _logger.LogInformation("Anomaly handled: {anomaly}", message.ToString());
        }
    }
}
