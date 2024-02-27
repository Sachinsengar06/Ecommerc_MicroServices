using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using JwtAuthenticationManager;
using CartService.BackGroundServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddScoped<ReceivedData>();
builder.Services.AddServiceDiscovery(o => o.UseEureka());
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
