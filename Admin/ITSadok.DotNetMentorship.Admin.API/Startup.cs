using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ITSadok.DotNetMentorship.Admin.Data;
using ITSadok.DotNetMentorship.Admin.API.Services;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ITSadok.DotNetMentorship.Admin.API;

public class Startup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;


    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _environment = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOptions<BotSettings>()
            .BindConfiguration(nameof(BotSettings));

        services.AddSingleton<BotService>();

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


        if (_environment.IsDevelopment())
        {
            services.AddHostedService<Services.TunnelService>();
        }

        services.AddScoped<UnitOfWork, UnitOfWork>();
        services.AddDbContext<AppDbContext>();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "ITSadok.DotNetMentorship.Admin API",
            });

            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

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
