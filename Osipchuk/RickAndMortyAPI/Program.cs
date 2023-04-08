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
builder.Services.AddTransient<ICharacterService, CharacterServices>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.Map("/", async (context) =>
{
    context.Response.WriteAsync("Start Page");
});

app.Map("/api/character/{id}", async (HttpContext context, int id) =>
{
    UnitOfWork unitOfWork = new UnitOfWork();
    string? character = JsonConvert.SerializeObject(await unitOfWork.Characters.GetCharacterAsync(id));
    context.Response.WriteAsync(character);
});

app.Map("/api/characters", async (HttpContext context) =>
{
    UnitOfWork unitOfWork = new UnitOfWork();
    var characters = new List<CharacterDTO>();
    await foreach (var character in unitOfWork.Characters.GetCharactersAsync())
    {
        characters.Add(character);
    }
    string data = JsonConvert.SerializeObject(characters);
    await context.Response.WriteAsync(data);
});

app.Run();