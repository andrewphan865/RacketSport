using Asp.Versioning;
using Catalog.Api;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogConnection")));

var app = builder.Build();

app.NewVersionedApi("Catalog").MapCatalogApiV1();

try
{
   await DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
