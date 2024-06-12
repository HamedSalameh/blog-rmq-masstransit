using Common;

namespace TelemetryService
{
    public class AnomalyDetector : IAnomalyDetector
    {
        public Task<bool> DetectAnomaliesAsync(TelemetryDataMessage message)
        {
            throw new NotImplementedException();
        }
    }
}