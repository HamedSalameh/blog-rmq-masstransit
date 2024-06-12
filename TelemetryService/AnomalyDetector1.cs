using Common;

public class AnomalyDetector : IAnomalyDetector
{
    private readonly ILogger<AnomalyDetector> _logger;

    public AnomalyDetector(ILogger<AnomalyDetector> logger)
    {
        _logger = logger;
    }

    public async Task<bool> DetectAnomaliesAsync(TelemetryDataMessage message)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Detecting anomalies in telemetry data message: {telemetryDataMessage}", message);
        }

        // Do some work here for detecting anomalies in telemetry data messages
        await Task.Delay(500);  // fake work ...

        return false;
    }
}