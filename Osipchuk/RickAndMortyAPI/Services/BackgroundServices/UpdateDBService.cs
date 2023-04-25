using Microsoft.EntityFrameworkCore;
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
        private readonly RickAndMortyContext _rickAndMortyContext;
        public UpdateDBService(HttpClient httpClient, UnitOfWork unitOfWork,
            RickAndMortyContext rickAndMortyContext)
        {
            _httpClient = httpClient;
            _rickAndMortyRepository = unitOfWork.Repository;
            _rickAndMortyContext = rickAndMortyContext;  
        }
        public async Task UpdateDBAsync()
        {
            var characters = await GetCharactersAsync();
            foreach (var character in characters)
            {
                if (!character.DateUpdated)
                {
                    character.DateUpdated = true;
                    bool isInDatabase = await _rickAndMortyContext.Characters.AnyAsync(ch => ch.Id == character.Id);
                    if (isInDatabase) await _rickAndMortyRepository.UpdateAsync(character);
                    else await _rickAndMortyRepository.CreateAsync(character);
                }
            }
        }
        public async Task<IList<Character>> GetCharactersAsync()
        {
            int countOfPage = 0;
            var characters = new List<Character>();
            var nextPageUrl = "https://rickandmortyapi.com/api/character";
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
            return characters;
        }
    }
}