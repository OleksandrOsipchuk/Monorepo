using Microsoft.AspNetCore.Http;
using RickAndMortyAPI;
using RickAndMortyAPI.Entities;
using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Services.AddSingleton<ILocationService, LocationService>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    var path = statusCodeContext.HttpContext.Request.Path;
    response.ContentType = "text/plain; charset=UTF-8";
    if (response.StatusCode == 403)
    {
        await response.WriteAsync($"Access to {path} denied.");
    }
    else if (response.StatusCode == 404)
    {
        await response.WriteAsync($"Resource {path} - Not Found.");
    }
});
//setting header manually?
app.Use(async (context, next) => { context.Request.Headers.Add("X-SecretKey", "PickleRick"); await next?.Invoke(); });
app.UseMiddleware<HeaderAuthenticationMiddleware>();

app.Map("/api/locations", async (HttpContext context, ILocationService locationService) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(await HTMLHelper.ToHTMLTable(locationService));
});

app.Map("/api/location/{ids}", async (HttpContext context, ILocationService locationService, string ids) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    await context.Response.WriteAsync(await HTMLHelper.ToHTMLTable(locationService, ids));
});
app.Run();