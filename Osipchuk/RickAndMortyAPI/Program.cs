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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
var app = builder.Build();

var services = new ServiceCollection();
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
    var character = JsonConvert.SerializeObject(await GetCharacterAsync(id));
    await response.WriteAsync(character);
});

app.Map("/api/characters", async (context) =>
{
    var response = context.Response;
    StringBuilder data = new StringBuilder();
    await foreach (var character in GetAllCharactersAsync())
    {
        string strCharacter = JsonConvert.SerializeObject(character);
        data.Append(strCharacter);
    }

    await response.WriteAsync(data.ToString());
});

async IAsyncEnumerable<Character> GetAllCharactersAsync()
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
    foreach(var character in characters)
    {
        yield return character;
    }
}
async Task<Character> GetCharacterAsync(string id)
{
    var character = await httpClient.GetFromJsonAsync<Character>($"https://rickandmortyapi.com/api/character/{id}");
    return character;
}
app.Run();