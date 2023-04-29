using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace RickAndMortyAPI.Services
{
    public class PullCharactersHostedService : BackgroundService
    {
        private readonly IPullCharactersJob _pullCharactersJob;
        private readonly IServiceScope scope;
        public PullCharactersHostedService(IServiceScopeFactory serviceScopeFactory)
        {
             scope = serviceScopeFactory.CreateScope();
            _pullCharactersJob = scope.ServiceProvider.GetRequiredService<IPullCharactersJob>();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _pullCharactersJob.RunAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

