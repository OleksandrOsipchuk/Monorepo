using Newtonsoft.Json;
using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./v1/swagger.json", "MyAPI V1");
});

app.UseMiddleware<HeaderAuthenticationMiddleware>();

app.MapGet("/api/locations", async (HttpContext context, [FromServices] ILocationService locationService) =>
{
    await foreach (var location in locationService.GetLocationsAsync())
        await context.Response.WriteAsync(JsonConvert.SerializeObject(location));
})
    .WithOpenApi(c => new(c) { Summary = "Get all locations" })
    .WithTags("Locations");

app.MapGet("/api/location/{id}", async (HttpContext context, [FromServices] ILocationService locationService,
    string id, [FromHeader(Name = "X-SecretKey")] string secretKey) =>
{
    await foreach (var location in locationService.GetLocationsAsync(id))
        await context.Response.WriteAsync(JsonConvert.SerializeObject(location));
})
    .WithOpenApi(c => new(c) { Summary = "Get locations by id." })
    .WithTags("Locations");

app.Run();