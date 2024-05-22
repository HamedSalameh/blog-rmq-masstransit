using Common;
using MassTransit;

namespace AuditServiceWorker
{
    public class AuditEventConsumer : IConsumer<AuditEvent>
    {
        private readonly ILogger<AuditEventConsumer> _logger;
        private readonly IAuditService _auditService;

        public AuditEventConsumer(ILogger<AuditEventConsumer> logger, IAuditService auditService)
        {
            _logger = logger;
            _auditService = auditService;
        }

        public async Task Consume(ConsumeContext<AuditEvent> context)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Consuming audit event: {auditEvent}", context.Message);
            }

            // Do some work here for auditing events
            await _auditService.WriteAuditEvent(context.Message);

            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Audit event consumed: {auditEvent}", context.Message);
            }

            await Task.CompletedTask;
        }
    }
}
