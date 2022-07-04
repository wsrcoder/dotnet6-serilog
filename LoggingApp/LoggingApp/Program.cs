
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

partial class Program
{

    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

       // Log.Logger.Information("Uma informação");

        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IGreetingService, GreetingService>();
                })
            .UseSerilog()
            .Build();

        var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);

        svc.Run();

        svc.Information("Uma informação");

        svc.Error("Um erro");
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("AppSettings.json", optional:false, reloadOnChange: true)
            .AddJsonFile($"AppSettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?? "production"}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    }
}
