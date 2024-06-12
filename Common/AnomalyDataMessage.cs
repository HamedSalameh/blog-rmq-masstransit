namespace Common
{
    public class AnomalyDataMessage
    {
        public AnomalyDataMessage(string deviceId, DateTime timestamp, string anomalyType, string anomalyDetails, TelemetryDataMessage originalTelemetryData)
        {
            DeviceId = deviceId;
            Timestamp = timestamp;
            AnomalyType = anomalyType;
            AnomalyDetails = anomalyDetails;
            OriginalTelemetryData = originalTelemetryData;
        }

        // Unique identifier for the device where the anomaly was detected
        public string DeviceId { get; set; }

        // Timestamp of when the anomaly was detected
        public DateTime Timestamp { get; set; }

        // Type or description of the anomaly detected
        public string AnomalyType { get; set; }

        // Detailed information about the anomaly
        public string AnomalyDetails { get; set; }

        // Optional: Original telemetry data that triggered the anomaly detection
        public TelemetryDataMessage OriginalTelemetryData { get; set; }


    }
}
