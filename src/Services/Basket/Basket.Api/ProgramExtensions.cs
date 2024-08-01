
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Basket.Api;

public static class ProgramExtensions
{
    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr();  
    }

    public static void AddCustomApplicationServices(this WebApplicationBuilder builder)
    {       

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        builder.Services.AddScoped<IBasketRepository, BasketRepository>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
    }

}
