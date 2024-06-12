namespace Common
{
    public class TelemetryDataMessage
    {
        // Unique identifier for the device sending the telemetry data
        public string DeviceId { get; set; }

        // Timestamp of when the telemetry data was recorded
        public DateTime Timestamp { get; set; }

        // The sensor data captured by the device. This can be a dictionary or a custom class
        // to represent various sensor readings. Here we use a dictionary for flexibility.
        public Dictionary<string, object> SensorData { get; set; }

        // Optional: Include metadata about the message, such as data quality or source
        public string DataQuality { get; set; }

        public TelemetryDataMessage()
        {
            SensorData = new Dictionary<string, object>();
        }
    }

}
