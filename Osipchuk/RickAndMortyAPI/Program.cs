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
using RickAndMortyAPI.IOHandler;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddTransient<ICharacterService, CharacterServices>();
builder.Services.AddTransient<IDTOService<Character, CharacterDTO>, CharacterDTOService>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.Map("/", async (context) =>
{
    context.Response.WriteAsync("Start Page");
});

app.Map("/api/character/{id}", async (HttpContext context,
    UnitOfWork unitOfWork, IDTOService<Character, CharacterDTO> dto, int id) =>
{
    var repository = unitOfWork.Repository;
    var character = await repository.GetCharacterAsync(id);
    string? data = JsonConvert.SerializeObject(dto.GetDTO(character));
    context.Response.WriteAsync(data);
});

app.Map("/api/characters", async (HttpContext context,
    UnitOfWork unitOfWork, IDTOService<Character, CharacterDTO> dto) =>
{
    var repository = unitOfWork.Repository;
    var characters = new List<Character>();
    await foreach (var character in repository.GetCharactersAsync())
    {
        characters.Add(character);
    }
    string data = JsonConvert.SerializeObject(dto.GetDTOs(characters));
    await context.Response.WriteAsync(data);
});

app.Run();