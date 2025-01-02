namespace Common
{
    public record class DeadLetterEvent(DateTime Timestamp, string Reason, TelemetryDataMessage OriginalMessage);
}
