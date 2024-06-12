using Common;
using TelemetryService;

public interface ITelemetryDataProcessor
{
    Task Process(TelemetryDataMessage message);
}
