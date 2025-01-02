using Common;
using MassTransit;
using System.Collections.Concurrent;

namespace AnomalyHandlingService
{
    public class AnomalyDataConsumer : IConsumer<Anomaly>
    {
        private readonly ILogger<AnomalyDataConsumer> _logger;
        private readonly IAnomalyDataHandler _anomalyDataHandler;

        private static readonly ConcurrentQueue<DateTime> _invocationTimes = new ConcurrentQueue<DateTime>();
        private readonly TimeSpan _timeWindow;
        private readonly int _threshold;

        public AnomalyDataConsumer(ILogger<AnomalyDataConsumer> logger, IAnomalyDataHandler anomalyDataHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _anomalyDataHandler = anomalyDataHandler ?? throw new ArgumentNullException(nameof(anomalyDataHandler));
            _timeWindow = TimeSpan.FromSeconds(20);
            _threshold = 3;
        }

        public async Task Consume(ConsumeContext<Anomaly> context)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Consuming anomaly data message: {AnomalyType}", context.Message.AnomalyType);
            }

            var now = DateTime.UtcNow;
            _invocationTimes.Enqueue(now);

            // Remove old invocation times
            while (_invocationTimes.TryPeek(out var oldest) && now - oldest > _timeWindow)
            {
                _invocationTimes.TryDequeue(out _);
            }

            // Safely check invocation count
            int invocationCount = _invocationTimes.Count;
            if (invocationCount > _threshold)
            {
                _logger.LogWarning("High frequency if anomalies detected: {InvocationCount}", invocationCount);

                var Alert = new Alert("High frequency of anomalies detected", now, invocationCount.ToString());
            }

            // Process anomaly data
            try
            {
                await _anomalyDataHandler.HandleAsync(context.Message);
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Anomaly data message processed successfully: {AnomalyType}", context.Message.AnomalyType);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to process anomaly data message: {AnomalyType}", context.Message.AnomalyType);
                throw;
            }
        }
    }
}
