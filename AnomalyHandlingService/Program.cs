using AnomalyHandlingService;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);

// Add MassTransit services
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AnomalyDataConsumer>();
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
builder.Services.AddSingleton<IAnomalyDataHandler, AnomalyDataHandler>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
