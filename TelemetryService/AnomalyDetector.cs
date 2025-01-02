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
        // detect anomalies in the telemetry data
        if (message.WaterMeasurementData.pHLevel < 6.5 || message.WaterMeasurementData.pHLevel > 8.5)
        {
            return await Task.FromResult(new Anomaly("pH", "pH level out of range", message));
        }
        if (message.WaterMeasurementData.Temperature < 0 || message.WaterMeasurementData.Temperature > 100)
        {
            return await Task.FromResult(new Anomaly("Temperature", "Temperature out of range", message));
        }
        if (message.WaterMeasurementData.NitrateConcentration < 0 || message.WaterMeasurementData.NitrateConcentration > 50)
        {
            return await Task.FromResult(new Anomaly("Nitrate", "Nitrate concentration out of range", message));
        }
        if (message.WaterMeasurementData.WaterLevel < 0 || message.WaterMeasurementData.WaterLevel > 100)
        {
            return await Task.FromResult(new Anomaly("WaterLevel", "Water level out of range", message));
        }
        if (message.WaterMeasurementData.DataQuality != "High")
        {
            return await Task.FromResult(new Anomaly("DataQuality", "Data quality not high", message));
        }

        return await Task.FromResult<Anomaly>(null!);
    }
}