using Newtonsoft.Json;
using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.Repository;

var builder = WebApplication.CreateBuilder();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("header", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-SecretKey"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "header"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<RickAndMortyContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./v1/swagger.json", "MyAPI V1");
});

app.UseMiddleware<HeaderAuthenticationMiddleware>();

app.MapGet("/api/locations", async (HttpContext context, ILocationService locationService) =>
{
    await foreach (var location in locationService.GetLocationsAsync())
        await context.Response.WriteAsync(JsonConvert.SerializeObject(location));
})
    .WithOpenApi(c => new(c) { Summary = "Get all locations" })
    .WithTags("Locations");
app.MapGet("/api/location", async (HttpContext context, ILocationService locationService,
    [FromQuery(Name = "locationsIDs")] int[] locationIDs ) =>
{
    await foreach (var location in locationService.GetLocationsAsync(locationIDs))
        await context.Response.WriteAsync(JsonConvert.SerializeObject(location));
})
    .WithOpenApi(c => new(c) { Summary = "Get locations by id." })
    .WithTags("Locations");

app.Run();