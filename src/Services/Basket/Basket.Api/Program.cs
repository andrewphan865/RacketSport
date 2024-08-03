var builder = WebApplication.CreateBuilder(args);

builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();
builder.Services.AddDaprClient();
builder.Services.AddApiVersioning();

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();
app.NewVersionedApi("Basket").MapBasketApiV1();
app.NewVersionedApi("Event").MapEventApiV1();

app.MapCustomHealthChecks("/hc", "/liveness", UIResponseWriter.WriteHealthCheckUIResponse);
app.Run();
