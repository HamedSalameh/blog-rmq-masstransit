using MassTransit;
using MessageProcessingService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ClinicalDataMessageConsumer>();
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

builder.Services.AddScoped<IClinicalDataService, ClinicalDataService>();
builder.Services.AddLogging();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
