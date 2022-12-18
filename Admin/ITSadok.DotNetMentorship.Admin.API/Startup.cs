using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using ITSadok.DotNetMentorship.Admin.Data;
using ITSadok.DotNetMentorship.Admin.API.Services;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using ITSadok.DotNetMentorship.Admin.Data.Repository;

namespace ITSadok.DotNetMentorship.Admin.API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        
    // Gets TelegramApi token from config file
    services.AddOptions<BotSettings>()
            .BindConfiguration(nameof(BotSettings));

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        };

        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.ContractResolver = contractResolver;
        });

        services.AddScoped<BotService>();
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
