using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Admin.Data.Repository;
using Admin.Data.Repository.Interfaces;
using Admin.Data.Entity.Json;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace TgAdmin
{
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
            string TelegramApiKey = config.GetConnectionString("TelegramApiKey");
            Console.WriteLine(TelegramApiKey);
            var OptionsBuilder = new DbContextOptionsBuilder<DbContext>();


            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(ConnectionString));
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen();

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
