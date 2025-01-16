using MassTransit;
using TelemetryService;

var builder = Host.CreateApplicationBuilder(args);

// Add MassTransit services
builder.Services.AddOptions<MassTransitHostOptions>()
    .Configure<IServiceProvider>((options, sp) =>
    {
        options.WaitUntilStarted = false;
        options.StartTimeout = TimeSpan.FromSeconds(10);
        options.StopTimeout = TimeSpan.FromSeconds(10);
        options.ConsumerStopTimeout = TimeSpan.FromSeconds(10);


        // Filters
        
    });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TelemetryDataConsumer>(configure =>
    {
        configure.UseMessageRetry(r => r.Immediate(5));
    });
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        cfg.ConfigureEndpoints(context);

        // filters
        cfg.UseKillSwitch(options =>
        {
            options.SetActivationThreshold(10);
            options.SetTripThreshold(0.15);
            options.SetRestartTimeout(TimeSpan.FromSeconds(30));
        });

        cfg.UseCircuitBreaker(cb =>
        {
            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
            cb.TripThreshold = 20;
            cb.ActiveThreshold = 10;
            cb.ResetInterval = TimeSpan.FromMinutes(5);
        });
    });
});

builder.Services.AddLogging();
builder.Services.AddSingleton<ITelemetryDataProcessor, TelemetryDataProcessor>();
builder.Services.AddSingleton<IAnomalyDetector, AnomalyDetector>();

var host = builder.Build();
host.Run();
