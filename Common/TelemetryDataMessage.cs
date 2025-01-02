namespace Common
{
    public class TelemetryDataMessage
    {
        // Unique identifier for the device sending the telemetry data
        public string DeviceId { get; set; }

        // Timestamp of when the telemetry data was recorded
        public DateTime Timestamp { get; set; }

        public WaterMeasurementData WaterMeasurementData { get; set; }

        // Optional: Include metadata about the message, such as data quality or source
        public string DataQuality { get; set; }

        public override string ToString()
        {
            return $"DeviceId: {DeviceId}, Timestamp: {Timestamp}, WaterMeasurementData: {WaterMeasurementData}";
        }
    }

}
