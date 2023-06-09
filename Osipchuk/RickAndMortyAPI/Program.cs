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
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<IPullCharactersJob, PullCharactersJob>();
builder.Services.AddHostedService<PullCharactersHostedService>();
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.ConfigureCustomExceptionMiddleware();
app.MapControllers();
app.Run();