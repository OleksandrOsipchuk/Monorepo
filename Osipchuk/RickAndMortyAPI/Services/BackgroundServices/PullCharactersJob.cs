using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RickAndMortyAPI.CharacterInfo;
using RickAndMortyAPI.Repository;
using RickMorty;
using System.Collections;

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
            await foreach (var page in GetCharactersAsync())
            {
                foreach (var character in page)
                {
                    try
                    {
                        await _rickAndMortyRepository.UpdateAsync(character);
                    }
                    catch(NullReferenceException)
                    {
                        _logger.LogInformation($"There is no character:" +
                            $" {character.Name} in db. Create new character.");
                        await _rickAndMortyRepository.CreateAsync(character);
                    }
                }
            }
            _logger.LogInformation("Success Database is updated!");
            await Task.Delay(TimeSpan.FromHours(1));
        }
        public async IAsyncEnumerable<IEnumerable<Character>> GetCharactersAsync()
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
                foreach (var character in ConvertFromCharacterAPI(result.Results))
                    characters.Add(character);
                nextPageUrl = result.Info.Next;
                yield return characters;
                countOfPage++;
            }
            var options = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(61)
            };
            _memoryCache.Set("nextPage", nextPageUrl, options);
        }
        public IEnumerable<Character> ConvertFromCharacterAPI(List<CharacterAPIResponse> apiCharacters)
        {
            foreach (var apiCharacter in apiCharacters)
            {
                Character character = new Character();
                character.Id = apiCharacter.Id;
                character.Name = apiCharacter.Name;
                character.Status = apiCharacter.Status;
                character.Species = apiCharacter.Species;
                character.Gender = apiCharacter.Gender;
                character.Status = apiCharacter.Image;
                yield return character;
            }
        }
    }
}