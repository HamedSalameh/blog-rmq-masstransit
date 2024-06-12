using Common;

public partial class TelemetryDataProcessor(ILogger<TelemetryDataProcessor> logger)
{
    public class AnomalyDetector : IAnomalyDetector
    {
        public Task<bool> DetectAnomaliesAsync(TelemetryDataMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
