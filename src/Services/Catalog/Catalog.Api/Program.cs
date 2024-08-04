var builder = WebApplication.CreateBuilder(args);

builder.AddCustomConfiguration();
builder.AddCustomSerilog();
builder.AddCustomHealthChecks();
builder.AddCustomDatabase();

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();


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