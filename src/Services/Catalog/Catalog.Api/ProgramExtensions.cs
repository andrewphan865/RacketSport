namespace Catalog.Api;

public static class ProgramExtensions
{
    private const string AppName = "Catalog API";


    public static void AddCustomConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddDaprSecretStore(
           "racketsport-secretstore",
           new DaprClientBuilder().Build());

    }

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

    public static void AddCustomDatabase(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CatalogContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogConnection")));
    }

    public static void AddCustomHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
                .AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddDapr()
                .AddNpgSql(builder.Configuration.GetConnectionString("CatalogConnection")!);
    }
}
