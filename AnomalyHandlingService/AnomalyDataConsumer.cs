using Common;
using MassTransit;

namespace AnomalyHandlingService
{
    public class AnomalyDataConsumer : IConsumer<Anomaly>
    {
        private readonly ILogger<AnomalyDataConsumer> _logger;
        private readonly IAnomalyDataHandler _anomalyDataHandler;

        public AnomalyDataConsumer(ILogger<AnomalyDataConsumer> logger, IAnomalyDataHandler anomalyDataHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _anomalyDataHandler = anomalyDataHandler ?? throw new ArgumentNullException(nameof(anomalyDataHandler));
        }

        public async Task Consume(ConsumeContext<Anomaly> context)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Consuming anomaly data message: {anomalyDataMessage}", context.Message);
            }

            // Process the anomaly data
            await _anomalyDataHandler.HandleAsync(context.Message);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Anomaly data message processed: {anomalyDataMessage}", context.Message);
            }
        }
    }
}
