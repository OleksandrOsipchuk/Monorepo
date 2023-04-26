using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RickAndMortyAPI.CharacterInfo;
using RickAndMortyAPI.Repository;
using RickMorty;

namespace RickAndMortyAPI.Services
{
    public class UpdateDBService : IUpdateDBService
    {
        private readonly HttpClient _httpClient;
        private readonly RickAndMortyRepository _rickAndMortyRepository;
        private readonly IMemoryCache _memoryCache;
        public UpdateDBService(HttpClient httpClient, UnitOfWork unitOfWork,IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _rickAndMortyRepository = unitOfWork.Repository;
            _memoryCache = memoryCache;
        }
        public async Task UpdateDBAsync()
        {
            var characters = await GetCharactersAsync();
            foreach (var character in characters)
            {
                bool isInDatabase = await _rickAndMortyRepository.IfExist(character.Id);
                if (isInDatabase) await _rickAndMortyRepository.UpdateAsync(character);
                else await _rickAndMortyRepository.CreateAsync(character);
            }
            _rickAndMortyRepository.ClearTracker();
        }
        public async Task<IList<Character>> GetCharactersAsync()
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
                countOfPage++;
            }
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(61)
            };
            _memoryCache.Set("nextPage", nextPageUrl, options);
            return characters;
        }
    }
}