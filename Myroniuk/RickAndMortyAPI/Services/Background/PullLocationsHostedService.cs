using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RickAndMortyAPI.Middleware;
using RickAndMortyAPI.Repository;

namespace RickAndMortyAPI.Services.Background
{
    public class PullLocationsHostedService : BackgroundService
    {
        private readonly IPullLocationsJob _job;
        private readonly ILogger<PullLocationsHostedService> _logger;
        private readonly IServiceScope _scope;
        public PullLocationsHostedService(IServiceScopeFactory serviceScopeFactory, ILogger<PullLocationsHostedService> logger) {
            _scope = serviceScopeFactory.CreateScope();
            _job = _scope.ServiceProvider.GetRequiredService<IPullLocationsJob>();
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _job.RunAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                _logger.LogInformation("Pulling job ended.");
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
