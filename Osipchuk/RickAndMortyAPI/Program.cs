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
using RickAndMortyAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RickAndMortyContext>(options =>
options.UseNpgsql(connection));
builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.Map("/", async (context) =>
{
    await context.Response.WriteAsync("Start Page");
});

app.Map("/api/character/{id}", async (HttpContext context, ICharacterService characterService, int id) =>
{
    string? data = JsonConvert.SerializeObject(await characterService.GetCharacterAsync(id));
    await context.Response.WriteAsync(data);
});

app.Map("/api/characters", async (HttpContext context, ICharacterService characterService) =>
{
    var characters = new List<CharacterDTO>();
    await foreach (var character in characterService.GetCharactersAsync())
    {
        characters.Add(character);
    }
    string data = JsonConvert.SerializeObject(characters);
    await context.Response.WriteAsync(data);
});

app.Run();