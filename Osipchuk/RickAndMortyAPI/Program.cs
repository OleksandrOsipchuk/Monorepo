using RickMorty;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using RickAndMortyAPI.CharacterInfo;
using Microsoft.Extensions.DependencyInjection;
using RickAndMortyAPI;
using RickAndMortyAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ICharacterService, CharacterServices>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    var path = statusCodeContext.HttpContext.Request.Path;

    response.ContentType = "text/plain; charset=UTF-8";
    if (response.StatusCode == 403)
    {
        await response.WriteAsync($"Path: {path}. Access Denied ");
    }
    else if (response.StatusCode == 404)
    {
        await response.WriteAsync($"Resource {path} Not Found");
    }
});

app.Map("/", async (context) =>
{
    context.Response.WriteAsync("Start Page");
});

app.Map("/api/character/{id}", async (HttpContext context,IHttpClientFactory httpClientFactory, string id) =>
{
    var httpClient = httpClientFactory?.CreateClient();
    var characterService = app.Services.GetService<ICharacterService>();
    var response = context.Response;
    var character = JsonConvert.SerializeObject(await characterService.GetCharacterAsync(httpClient, id));
    await response.WriteAsync(character);
});

app.Map("/api/characters", async (HttpContext context, IHttpClientFactory httpClientFactory) =>
{
    var httpClient = httpClientFactory?.CreateClient();
    var characterService = app.Services.GetService<ICharacterService>();
    var response = context.Response;
    List<Character> characters = new List<Character>();
    await foreach (var character in characterService.GetAllCharactersAsync(httpClient))
    {
        characters.Add(character);
    }
    string data = JsonConvert.SerializeObject(characters);
    await response.WriteAsync(data.ToString());
});

app.Run();