using Common;
using MassTransit;
using System.Text.Json;

namespace DataInjectorSim
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBus _bus;

        public Worker(ILogger<Worker> logger, IBus bus)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                var data = GetTelemetryDataMessage();

                var serializedData = JsonSerializer.Serialize(data);
                _logger.LogDebug("Publishing telemetry data message: {telemetryDataMessage}", serializedData);

                await _bus.Publish(data, cancellationToken);

                await Task.Delay(3000, cancellationToken);
            }
        }

        public TelemetryDataMessage GetTelemetryDataMessage()
        {
            var random = new Random();
            var deviceIdList = new List<string> { "Device-1", "Device-2", "Device-3" };

            var deviceId = deviceIdList[random.Next(0, deviceIdList.Count)];
            var timestamp = DateTime.Now;

            var telemteryDataMessage = new TelemetryDataMessage()
            {
                DeviceId = deviceId,
                Timestamp = timestamp,
                WaterMeasurementData = GetWaterMeasurementData()
            };

            return telemteryDataMessage;
        }

        private static WaterMeasurementData GetWaterMeasurementData()
        {
            var random = new Random();
            var waterMeasurementData = new WaterMeasurementData
            {
                WaterLevel = random.NextDouble() * 100, // 0 to 100 cm
                pHLevel = Math.Round(random.NextDouble() * 14, 2), // 0 to 14 pH
                Temperature = Math.Round(random.NextDouble() * 40, 2), // 0 to 40 °C
                NitrateConcentration = Math.Round(random.NextDouble() * 50, 2), // 0 to 50 mg/L
                DataQuality = "High"
            };

            // Occasionally inject anomalies
            if (random.Next(0, 10) < 3) // 30% chance to inject an anomaly
            {
                waterMeasurementData = InjectAnomaly(waterMeasurementData);
                waterMeasurementData.DataQuality = "Low";
            }

            return waterMeasurementData;
        }

        private static WaterMeasurementData InjectAnomaly(WaterMeasurementData waterMeasurementData)
        {
            var random = new Random();
            var anomalyType = random.Next(0, 4); // 0 to 3

            switch (anomalyType)
            {
                case 0:
                    // Inject an anomaly for WaterLevel (e.g., negative or excessively high value)
                    waterMeasurementData.WaterLevel = random.NextDouble() > 0.5 ? -random.NextDouble() * 100 : random.NextDouble() * 200;
                    break;
                case 1:
                    // Inject an anomaly for pHLevel (e.g., negative or above 14)
                    waterMeasurementData.pHLevel = random.NextDouble() > 0.5 ? -Math.Round(random.NextDouble() * 14, 2) : Math.Round(random.NextDouble() * 28, 2);
                    break;
                case 2:
                    // Inject an anomaly for Temperature (e.g., negative or excessively high value)
                    waterMeasurementData.Temperature = random.NextDouble() > 0.5 ? -Math.Round(random.NextDouble() * 40, 2) : Math.Round(random.NextDouble() * 80, 2);
                    break;
                case 3:
                    // Inject an anomaly for NitrateConcentration (e.g., negative or excessively high value)
                    waterMeasurementData.NitrateConcentration = random.NextDouble() > 0.5 ? -Math.Round(random.NextDouble() * 50, 2) : Math.Round(random.NextDouble() * 100, 2);
                    break;
            }

            return waterMeasurementData;
        }
    }
}
