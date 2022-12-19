﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ITSadok.DotNetMentorship.Admin.Data;
using ITSadok.DotNetMentorship.Admin.API.Services;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace ITSadok.DotNetMentorship.Admin.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private IWebHostEnvironment _CurrentEnvironment { get; set; }


    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _CurrentEnvironment = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Gets Telegram token and passes it to options
        services.AddOptions<BotSettings>()
            .BindConfiguration(nameof(BotSettings));

        // Adds BotService 9for TelegramBotClient) to DI Container
        services.AddSingleton<BotService>();

        // Sers CamelCase naming policy for JSON
        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        // Configures NewtonsoftJson
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = contractResolver;
        });


        if (_CurrentEnvironment.IsDevelopment())
        {
            services.AddHostedService<Services.TunnelService>();
        }

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSwaggerGen();
        services.AddDbContext<AppDbContext>();


    }
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
        }
        app.MapControllers();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseAuthorization();
        app.Run();
    }
}
