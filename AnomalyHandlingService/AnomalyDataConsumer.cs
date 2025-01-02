using Common;
using MassTransit;
using System.Collections.Concurrent;

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
            if (context.Message == null)
            {
                _logger.LogError("Received null anomaly data message.");
                throw new ArgumentNullException(nameof(context.Message));
            }

            _logger.LogDebug("Consuming anomaly data message: {AnomalyType}", context.Message.AnomalyType);

            try
            {
                await _anomalyDataHandler.HandleAsync(context.Message);

                _logger.LogInformation("Anomaly data message processed successfully: {AnomalyType}", context.Message.AnomalyType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process anomaly data message: {AnomalyType}. Message: {@Message}", context.Message.AnomalyType, context.Message);
                throw;
            }
        }
    }
}
