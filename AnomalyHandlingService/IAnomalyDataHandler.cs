using Common;

namespace AnomalyHandlingService
{
    public interface IAnomalyDataHandler
    {
        Task HandleAsync(AnomalyDataMessage message);
    }
}
