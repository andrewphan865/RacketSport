
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.AddCustomHealthChecks();
builder.AddCustomApplicationServices();
builder.Services.AddDaprClient();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IIdentityService, IdentityService>();



app.Run();
