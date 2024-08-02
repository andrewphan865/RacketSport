
var builder = WebApplication.CreateBuilder(args);


builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();
builder.Services.AddDaprClient();

var app = builder.Build();





app.Run();
