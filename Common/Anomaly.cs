namespace Common
{
    public class Anomaly
    {

        public Anomaly(string anomalyType, string anomalyDetails, TelemetryDataMessage originalTelemetryData)
        {
            AnomalyType = anomalyType;
            AnomalyDetails = anomalyDetails;
            OriginalTelemetryData = originalTelemetryData;

        }

        // Type or description of the anomaly detected
        public string AnomalyType { get; set; }

        // Detailed information about the anomaly
        public string AnomalyDetails { get; set; }

        // Optional: Original telemetry data that triggered the anomaly detection
        public TelemetryDataMessage OriginalTelemetryData { get; set; }

        public override string ToString()
        {
            return $"AnomalyType: {AnomalyType}, AnomalyDetails: {AnomalyDetails}, OriginalTelemetryData: {OriginalTelemetryData}";
        }
    }
}
