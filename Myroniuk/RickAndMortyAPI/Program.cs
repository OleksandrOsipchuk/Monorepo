using Newtonsoft.Json;
using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
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

app.MapGet("/api/locations", async (HttpContext context, RickAndMortyContext dbcontext,
     [FromHeader(Name = "X-SecretKey")] string secretKey) =>
{
    var locations = JsonConvert.SerializeObject(await dbcontext.Locations.ToListAsync());
    await context.Response.WriteAsync(locations);
})
    .WithOpenApi(c => new(c) { Summary = "Get all locations" })
    .WithTags("Locations");

app.MapGet("/api/location/{id}", async (HttpContext context, RickAndMortyContext dbcontext,
    string locationid, [FromHeader(Name = "X-SecretKey")] string secretKey) =>
{
    var idList = locationid.Split(",").Select(int.Parse).ToList();
    foreach (var id in idList) {
        var location = await dbcontext.Locations.FirstOrDefaultAsync(x => x.Id == id);
        if (location != null) await context.Response.WriteAsync(JsonConvert.SerializeObject(location));
        else context.Response.StatusCode = 401; 
    }
})
    .WithOpenApi(c => new(c) { Summary = "Get locations by id." })
    .WithTags("Locations");

app.Run();