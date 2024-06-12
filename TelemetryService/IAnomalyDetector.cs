using Common;

public interface IAnomalyDetector
{
    Task<bool> DetectAnomaliesAsync(TelemetryDataMessage message);
}
