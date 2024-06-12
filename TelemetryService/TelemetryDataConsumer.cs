using Common;
using MassTransit;
using TelemetryService;

public sealed class TelemetryDataConsumer : IConsumer<TelemetryDataMessage>
{
    private readonly ILogger<TelemetryDataConsumer> _logger;
    private readonly ITelemetryDataProcessor _telemtryDataProcessor;

    public TelemetryDataConsumer(ILogger<TelemetryDataConsumer> logger, ITelemetryDataProcessor telemtryDataProcessor)
    {
        _logger = logger;
        _telemtryDataProcessor = telemtryDataProcessor;
    }

    public async Task Consume(ConsumeContext<TelemetryDataMessage> context)
    {
        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.LogDebug("Consuming telemetry data message: {telemetryDataMessage}", context.Message);
        }

        validateMessage(context);

        await _telemtryDataProcessor.Process(context.Message);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Telemetry data message processed: {telemetryDataMessage}", context.Message);
        }

        var telemetryData = context.Message;
        if (telemetryData != null)
        {
            await _telemtryDataProcessor.Process(telemetryData);

            // publish audit event
            await context.Publish(new AuditEvent(DateTime.Now, AuditAction.Processed, telemetryData.DeviceId));
        }
    }

    private void validateMessage(ConsumeContext<TelemetryDataMessage> context)
    {
        // Some basic message validation
        if (context.Message == null)
        {
            _logger.LogError("Invalid telemetry data message: {telemetryDataMessage}", context.Message);
            return;
        }

        // in case of message validation failure, publish the message to a dead-letter queue
        if (string.IsNullOrWhiteSpace(context.Message.DeviceId) ||
            context.Message.Timestamp == default ||
            context.Message.SensorData == null)
        {
            // publish the letter to the dead letter exchange
            _logger.LogWarning("Invalid telemetry data: {telemetryDataMessage}", context.Message);

            // await context.Publish(new DeadLetterEvent(DateTime.Now, "Invalid telemetry data message", context.Message);

            return;
        }
    }
}