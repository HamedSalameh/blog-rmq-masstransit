using Common;

namespace TelemetryService
{
    public interface IAnomalyDetector
    {
        Task<Anomaly> DetectAnomaliesAsync(TelemetryDataMessage message);
    }
}