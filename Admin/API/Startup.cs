using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TgModerator.Data.Repository;
using TgModerator.Data.Repository.IRepository;

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
            var ConfigBuilder = new ConfigurationBuilder();
            ConfigBuilder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            ConfigBuilder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = ConfigBuilder.Build();
            // получаем строку подключения
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<StudentContext>();

            services.AddDbContext<StudentContext>(options => options.UseNpgsql(connectionString));
            services.AddSwaggerGen();
            services.AddControllers().AddNewtonsoftJson();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
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
