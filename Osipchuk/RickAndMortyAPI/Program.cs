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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using RickAndMortyAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<ICharacterService, CharacterServices>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<RickAndMortyContext>(options => options.UseNpgsql(connection));
var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.Map("/", async (context) =>
{
    context.Response.WriteAsync("Start Page");
});

app.Map("/api/character/{id}", async (HttpContext context, RickAndMortyContext db, int id) =>
{
    string? character = JsonConvert.SerializeObject(await db.Characters.FirstOrDefaultAsync(c => c.Id == id));
    if (character != null) context.Response.WriteAsync(character);
    else context.Response.StatusCode = 401;
});

app.Map("/api/characters", async (HttpContext context, RickAndMortyContext db) =>
{
    var characters = JsonConvert.SerializeObject(await db.Characters.ToListAsync());
    await context.Response.WriteAsync(characters);
});

//app.Map("/api/character/{id}", async (HttpContext context, IHttpClientFactory httpClientFactory, ICharacterService characterService, int id) =>
//{
//    var httpClient = httpClientFactory?.CreateClient();
//    var response = context.Response;
//    var character = JsonConvert.SerializeObject(await characterService.GetCharacterAsync(id));
//    await response.WriteAsync(character);
//});

//app.Map("/api/characters", async (HttpContext context, IHttpClientFactory httpClientFactory, ICharacterService characterService) =>
//{
//    var httpClient = httpClientFactory?.CreateClient();
//    var response = context.Response;
//    List<Character> characters = new List<Character>();
//    await foreach (var character in characterService.GetAllCharactersAsync())
//    {
//        characters.Add(character);
//    }
//    string data = JsonConvert.SerializeObject(characters);
//    await response.WriteAsync(data.ToString());
//});
app.Run();