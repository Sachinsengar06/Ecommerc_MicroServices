using JwtAuthenticationManager;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<JwtTokenHandler>();
builder.Services.AddServiceDiscovery(o => o.UseEureka());


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
