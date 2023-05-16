using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Services;
using RickAndMortyAPI.Repository;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.Services.Background;

var builder = WebApplication.CreateBuilder();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddTransient<IPullLocationsJob, PullLocationsJob>();
builder.Services.AddHostedService<PullLocationsHostedService>();
builder.Services.AddMemoryCache();
builder.Services.AddTransient<UnitOfWork>();
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("header", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Name = "X-SecretKey"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "header"
                }
            },
            new List<string>()
        }
    });
});
builder.Services.AddDbContext<RickAndMortyContext>
    (options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./v1/swagger.json", "MyAPI V1");
});

app.UseMiddleware<HeaderAuthenticationMiddleware>();
app.MapControllers();
app.Run();