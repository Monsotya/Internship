using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
    services.AddSingleton<MonitorLoop>();
    services.AddHostedService<QueuedHostedService>();
    services.AddSingleton<IBackgroundTaskQueue>(_ => new DefaultBackgroundTaskQueue(100));
    })
    .Build();
MonitorLoop monitorLoop = host.Services.GetRequiredService<MonitorLoop>()!;
monitorLoop.StartMonitorLoop();
await host.RunAsync();
