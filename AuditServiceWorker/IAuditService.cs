using Common;

namespace AuditServiceWorker
{
    public interface IAuditService
    {
        Task WriteAuditEvent(AuditEvent auditEvent);
    }
}
