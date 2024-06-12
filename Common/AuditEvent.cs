namespace Common
{
    public record AuditEvent(DateTime DateTime, AuditAction action, string Message);
}
