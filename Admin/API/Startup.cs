using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Admin.Data.Repository;
using Admin.Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Admin.Data.Entity;
using Newtonsoft.Json.Serialization;

namespace TgAdmin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection Services)
        {
            // Takes config from file
            var ConfigBuilder = new ConfigurationBuilder();
            ConfigBuilder.SetBasePath(Directory.GetCurrentDirectory());
            ConfigBuilder.AddJsonFile("appsettings.json");
            var config = ConfigBuilder.Build();
            string ConnectionString = config.GetConnectionString("DefaultConnection");
            var OptionsBuilder = new DbContextOptionsBuilder<DbContext>();

            // Gets TelegramApi token from config file
            var appOptions = new AppProperties();
            Configuration.GetSection(AppProperties.PropertiesSection).Bind(appOptions);
            string TelegramApiKey = appOptions.TelegramApiKey;

            // Creates instance for TelegramClient and adds it to DI container
            TelegramBotClient TgClient = new TelegramBotClient(TelegramApiKey);

            Services.AddDbContext<AppDbContext>(Options => Options.UseNpgsql(ConnectionString));

            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
            Services.AddControllers().AddNewtonsoftJson(Options =>
            {
                Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                Options.SerializerSettings.ContractResolver = contractResolver;
            });
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddSwaggerGen();

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
}
