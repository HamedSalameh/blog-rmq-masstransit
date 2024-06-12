using Common;
using MassTransit;

namespace AnomalyHandlingService
{
    public class AnomalyDataConsumer : IConsumer<AnomalyDataMessage>
    {
        private readonly ILogger<AnomalyDataConsumer> _logger;
        private readonly IAnomalyDataHandler _anomalyDataHandler;

        public AnomalyDataConsumer(ILogger<AnomalyDataConsumer> logger, IAnomalyDataHandler anomalyDataHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _anomalyDataHandler = anomalyDataHandler ?? throw new ArgumentNullException(nameof(anomalyDataHandler));
        }

        public async Task Consume(ConsumeContext<AnomalyDataMessage> context)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Consuming anomaly data message: {anomalyDataMessage}", context.Message);
            }

            // Process the anomaly data
            await _anomalyDataHandler.HandleAsync(context.Message);

            // publish audit event
            await context.Publish(new AuditEvent(DateTime.Now, AuditAction.Processed, context.Message.DeviceId));

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Anomaly data message processed: {anomalyDataMessage}", context.Message);
            }
        }
    }
}
