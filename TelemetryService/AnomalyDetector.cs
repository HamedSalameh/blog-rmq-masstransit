using Common;
using TelemetryService;

public class AnomalyDetector : IAnomalyDetector
{
    private readonly ILogger<AnomalyDetector> _logger;

    public AnomalyDetector(ILogger<AnomalyDetector> logger)
    {
        _logger = logger;
    }

    public async Task<Anomaly> DetectAnomaliesAsync(TelemetryDataMessage message)
    {
        await Task.Delay(500);  // fake work ...

        return await Task.FromResult<Anomaly>(null!);
    }
}