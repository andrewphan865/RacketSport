
using Basket.Api.Infrastructure.IntegrationEvents.EventHandling;
using EventBus;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace Basket.Api;

public static class ProgramExtensions
{
    private const string AppName = "Basket API";

    public static void AddCustomSerilog(this WebApplicationBuilder builder)
    {
        var seqServerUrl = builder.Configuration["SeqServerUrl"];

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .WriteTo.Console()
            .WriteTo.Seq(seqServerUrl!)
            .Enrich.WithProperty("ApplicationName", AppName)
            .CreateLogger();

        builder.Host.UseSerilog();
    }
    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr();  
    }

    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEventBus, DaprEventBus>();
        builder.Services.AddScoped<OrderStatusChangedToSubmittedIntegrationEventHandler>();

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
    }

}
