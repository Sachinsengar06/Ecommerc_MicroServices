using Ocelot.DependencyInjection;
using JwtAuthenticationManager;
using Ocelot.Middleware;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration).AddEureka().AddPolly();
builder.Services.AddServiceDiscovery(o => o.UseEureka());

builder.Services.AddCustomJwtAuthentication();
var app = builder.Build();

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
