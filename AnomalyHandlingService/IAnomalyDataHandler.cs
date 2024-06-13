using Common;

namespace AnomalyHandlingService
{
    public interface IAnomalyDataHandler
    {
        Task HandleAsync(Anomaly message);
    }
}
