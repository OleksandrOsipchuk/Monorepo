using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace RickAndMortyAPI.Services
{
    public class UpdateDBHostedService : BackgroundService
    {
        private readonly ILogger<UpdateDBHostedService> _logger;
        private readonly IUpdateDBService _updateDBService;
        private readonly IServiceScope scope;
        public UpdateDBHostedService(ILogger<UpdateDBHostedService> logger, 
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
             scope = serviceScopeFactory.CreateScope();
            _updateDBService = scope.ServiceProvider.GetRequiredService<IUpdateDBService>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _updateDBService.UpdateDBAsync();
                    _logger.LogInformation("Success Database is updated!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                await Task.Delay(TimeSpan.FromHours(1));
            }
        }
    }
}

