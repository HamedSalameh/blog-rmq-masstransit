using Common;

namespace AuditServiceWorker
{
    public class AuditService(ILogger<AuditService> logger) : IAuditService
    {
        private readonly ILogger<AuditService> _logger = logger;

        public Task WriteAuditEvent(AuditEvent auditEvent)
        {
            _logger.LogInformation("Writing audit event: {auditEvent}", auditEvent);

            // Do some work here for auditing events
            return Task.CompletedTask;
        }
    }
}
