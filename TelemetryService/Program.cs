using MassTransit;
using TelemetryService;

var builder = Host.CreateApplicationBuilder(args);

// Add MassTransit services
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TelemetryDataConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddLogging();
builder.Services.AddSingleton<ITelemetryDataProcessor, TelemetryDataProcessor>();
builder.Services.AddSingleton<IAnomalyDetector, AnomalyDetector>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
