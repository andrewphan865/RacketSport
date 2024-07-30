using Catalog.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CatalogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogConnection")));

var app = builder.Build();

try
{
   await DbInitializer.InitDb(app);
}
catch (Exception e)
{
    Console.WriteLine(e);
}

app.Run();
                          