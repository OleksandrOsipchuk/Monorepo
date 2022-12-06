using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Newtonsoft.Json.Serialization;
using Data.Repository;
using Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using API.Services;

namespace API;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Takes config from file
        var ConfigBuilder = new ConfigurationBuilder();
        ConfigBuilder.SetBasePath(Directory.GetCurrentDirectory());
        ConfigBuilder.AddJsonFile("appsettings.json");
        var config = ConfigBuilder.Build();
        string ConnectionString = config.GetConnectionString("DefaultConnection");
        var OptionsBuilder = new DbContextOptionsBuilder<DbContext>();

        // Gets TelegramApi token from config file
        var BotSettings = new BotSettings();
        services.Configure<BotSettings>(Configuration.GetSection(BotSettings.PropertiesSection));

        services.AddDbContext<AppDbContext>(Options => Options.UseNpgsql(ConnectionString));

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
        services.AddMvc();
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
