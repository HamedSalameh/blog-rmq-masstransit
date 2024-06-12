using Common;
using MassTransit;

public partial class TelemetryDataProcessor(ILogger<TelemetryDataProcessor> logger, IAnomalyDetector anomalyDetector, IBus bus) : ITelemetryDataProcessor
{
    private readonly ILogger<TelemetryDataProcessor> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IAnomalyDetector _anomalyDetector = anomalyDetector ?? throw new ArgumentNullException(nameof(anomalyDetector));
    private readonly IBus _bus = bus ?? throw new ArgumentNullException(nameof(bus));

    public async Task Process(TelemetryDataMessage message)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Processing telemetry data message: {telemetryDataMessage}", message);
        }

        // detect anomalies in the telemetry data
        var isAnomalyDetected = await _anomalyDetector.DetectAnomaliesAsync(message);

        if (isAnomalyDetected)
        {
            // publish anomaly data message
            //await _bus.Publish(new AnomalyDataMessage(message.DeviceId, message.Timestamp)

            _logger.LogWarning("Anomaly detected in telemetry data: {telemetryDataMessage}", message);
        }

        // Do some work here for processing telemetry data messages
        await Task.Delay(500);  // fake work ...
    }
}
