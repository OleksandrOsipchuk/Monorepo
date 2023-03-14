using RickMorty;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using RickAndMortyAPI.CharacterInfo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var services = new ServiceCollection();
services.AddHttpClient();
var serviceProvider = services.BuildServiceProvider();
var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
var httpClient = httpClientFactory?.CreateClient();

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

app.Map("/api/character/{id}", async (HttpContext context, string id) =>
{
    var response = context.Response;
    var character = await httpClient.GetFromJsonAsync<Character>($"https://rickandmortyapi.com/api/character/{id}");
    await response.WriteAsJsonAsync(character.Name + character.Status + character.Species + character.Gender);
});

app.Map("/api/characters", async (context) =>
{
    var response = context.Response;
    List<Character> characters = await GetAllCharactersAsync();
    StringBuilder data = new StringBuilder();
    foreach (var character in characters)
    {
        data.Append(character.Id + character.Name + character.Status + character.Species + character.Gender);
    }
    response.WriteAsJsonAsync(data.ToString());
});

async Task<List<Character>> GetAllCharactersAsync()
{
    var characters = new List<Character>();
    var nextPageUrl = "https://rickandmortyapi.com/api/character";
    while (!string.IsNullOrEmpty(nextPageUrl))
    {
        var response = await httpClient.GetAsync(nextPageUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        characters.AddRange(result.Results);
        nextPageUrl = result.Info.Next;
    }
    return characters;
}

app.Run();