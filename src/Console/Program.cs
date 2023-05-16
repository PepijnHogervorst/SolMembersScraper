using Console.Services.Initialization;
using Console.Services.SolScraper;
using Console.Services.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.ConfigureServices();
    })
    .UseSerilog()
    .Build();

Log.Logger.Information("Application starting");

Startup startup = ActivatorUtilities.CreateInstance<Startup>(host.Services);
await startup.InitializeAsync();

ISolScraperProgram svc = ActivatorUtilities.CreateInstance<SolScraperProgram>(host.Services);
svc.Run();
