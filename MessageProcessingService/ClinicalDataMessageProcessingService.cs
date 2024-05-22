using Common;
using MassTransit;


namespace MessageProcessingService
{
    internal class ClinicalDataService(ILogger<ClinicalDataService> logger, IBus bus) : IClinicalDataService
    {
        private readonly ILogger<ClinicalDataService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IBus _bus = bus ?? throw new ArgumentNullException(nameof(bus));

        public async Task Process(ClinicalDataMessage message)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Processing clinical data message: {clinicalDataMessage}", message);
            }

            // Do some work here for pocessing clinical data messages
            await Task.Delay(500);  // fake work ...

            // publish audit event
            await _bus.Publish(new AuditEvent(DateTime.Now, "Processed", message.ToString()));
        }
    }

    internal interface IClinicalDataService
    {
        Task Process(ClinicalDataMessage message);
    }

    internal class ClinicalDataMessageConsumer(IClinicalDataService clinicalDataService, ILogger<ClinicalDataMessageConsumer> logger) : IConsumer<ClinicalDataMessage>
    {
        private readonly ILogger<ClinicalDataMessageConsumer> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IClinicalDataService _clinicalDataService = clinicalDataService ?? throw new ArgumentNullException(nameof(clinicalDataService));

        public async Task Consume(ConsumeContext<ClinicalDataMessage> context)
        {
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug("Consuming clinical data message: {clinicalDataMessage}", context.Message);
            }

            // Do some work here for pocessing clinical data messages
            await _clinicalDataService.Process(context.Message);
        }
    }
}
