var builder = WebApplication.CreateBuilder(args);

builder.AddCustomSerilog();
builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();

builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();
app.NewVersionedApi("Basket").MapBasketApiV1();
app.NewVersionedApi("Event").MapEventApiV1();

app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);
app.Run();
