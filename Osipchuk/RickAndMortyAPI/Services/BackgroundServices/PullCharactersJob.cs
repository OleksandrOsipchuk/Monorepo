using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RickAndMortyAPI.CharacterInfo;
using RickAndMortyAPI.Repository;
using RickMorty;

namespace RickAndMortyAPI.Services
{
    public class PullCharactersJob : IPullCharactersJob
    {
        private readonly HttpClient _httpClient;
        private readonly RickAndMortyRepository _rickAndMortyRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<PullCharactersJob> _logger;
        public PullCharactersJob(HttpClient httpClient, UnitOfWork unitOfWork,
            IMemoryCache memoryCache, ILogger<PullCharactersJob> logger)
        {
            _httpClient = httpClient;
            _rickAndMortyRepository = unitOfWork.Repository;
            _memoryCache = memoryCache;
            _logger = logger;
        }
        public async Task RunAsync()
        {
            await foreach (var character in GetCharactersAsync())
            {
                bool isInDatabase = await _rickAndMortyRepository.CheckIfExist(character.Id);
                if (isInDatabase) await _rickAndMortyRepository.UpdateAsync(character);
                else await _rickAndMortyRepository.CreateAsync(character);
            }
            _logger.LogInformation("Success Database is updated!");
            _rickAndMortyRepository.ClearTracker();
            await Task.Delay(TimeSpan.FromHours(1));
        }
        public async IAsyncEnumerable<Character> GetCharactersAsync()
        {
            int countOfPage = 0;
            var characters = new List<Character>();
            if (!_memoryCache.TryGetValue("nextPage", out string? nextPageUrl) || string.IsNullOrEmpty(nextPageUrl))
            {
                nextPageUrl = "https://rickandmortyapi.com/api/character";
            }
            while (!string.IsNullOrEmpty(nextPageUrl) && countOfPage <= 4)
            {
                var response = await _httpClient.GetAsync(nextPageUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                characters.AddRange(result.Results);
                nextPageUrl = result.Info.Next;
                foreach (var character in characters)
                {
                    yield return character;
                }
                countOfPage++;
            }
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(61)
            };
            _memoryCache.Set("nextPage", nextPageUrl, options);
        }
    }
}