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

        if (!await ValidateMessageAsync(context))
        {
            return;
        }

        await _telemtryDataProcessor.Process(context.Message);

        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Telemetry data message processed: {telemetryDataMessage}", context.Message);
        }
    }

    private async Task<bool> ValidateMessageAsync(ConsumeContext<TelemetryDataMessage> context)
    {
        if (context.Message == null)
        {
            _logger.LogError("Received null telemetry data message.");
            await context.Publish(new DeadLetterEvent(
                Timestamp: DateTime.UtcNow,
                Reason: "Null message",
                OriginalMessage: null));
            return false;
        }

        if (string.IsNullOrWhiteSpace(context.Message.DeviceId) ||
            context.Message.Timestamp == default ||
            context.Message.WaterMeasurementData == null)
        {
            _logger.LogWarning("Invalid telemetry data for DeviceId: {DeviceId} at {Timestamp}",
                context.Message.DeviceId, context.Message.Timestamp);
            await context.Publish(new DeadLetterEvent(
                Timestamp: DateTime.UtcNow,
                Reason: "Invalid telemetry data",
                OriginalMessage: context.Message));
            return false;
        }

        return true;
    }
}