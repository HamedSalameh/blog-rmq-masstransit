namespace Common
{
    public class WaterMeasurementData
    {
        public double WaterLevel { get; set; } // in centimeters
        public double pHLevel { get; set; } // pH level of water
        public double Temperature { get; set; } // in Celsius
        public double NitrateConcentration { get; set; } // in mg/L
        public string DataQuality { get; set; } // High, Medium, Low
    }
}
