using Common;
using MassTransit;
using TelemetryService;

public class TelemetryDataProcessor(ILogger<TelemetryDataProcessor> logger, IAnomalyDetector anomalyDetector, IBus bus) : ITelemetryDataProcessor
{
    private readonly ILogger<TelemetryDataProcessor> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IAnomalyDetector _anomalyDetector = anomalyDetector ?? throw new ArgumentNullException(nameof(anomalyDetector));
    private readonly IBus _bus = bus ?? throw new ArgumentNullException(nameof(bus));

    public async Task Process(TelemetryDataMessage message)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Processing telemetry data message: {telemetryDataMessage}", message.ToString());
        }

        // detect anomalies in the telemetry data
        var anomaly = await _anomalyDetector.DetectAnomaliesAsync(message);

        if (anomaly != null)
        {
            _logger.LogWarning("Anomaly detected: {anomaly}", anomaly.AnomalyType);

            // publish the anomaly to the message broker
            await _bus.Publish(anomaly);
        }
        else
        {
            // do some work here to process the telemetry data message
            await Task.Delay(500);  // fake work ...

            _logger.LogInformation("Telemetry data message processed: {telemetryDataMessage}", message);
        }
    }
}
