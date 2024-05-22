namespace Common
{
    public record AuditEvent(DateTime DateTime, string action, string Message);
}
