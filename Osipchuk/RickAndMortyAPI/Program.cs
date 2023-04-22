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
builder.Services.AddControllers();
builder.Services.AddDbContext<RickAndMortyContext>(options =>
options.UseNpgsql(connection));
builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddHttpClient();


var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Character}/{action=GetStartPage}/{id?}");

app.Run();