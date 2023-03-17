using Newtonsoft.Json;
using RickAndMortyAPI.CharacterInfo;
using RickMorty;

namespace RickAndMortyAPI.Services
{
    public class CharacterServices : ICharacterService
    {
        public async IAsyncEnumerable<Character> GetAllCharactersAsync(HttpClient httpClient)
        {
            var characters = new List<Character>();
            var nextPageUrl = "https://rickandmortyapi.com/api/character";
            while (!string.IsNullOrEmpty(nextPageUrl))
            {
                var response = await httpClient.GetAsync(nextPageUrl);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
                characters.AddRange(result.Results);
                nextPageUrl = result.Info.Next;
            }
            foreach (var character in characters)
            {
                yield return character;
            }
        }

        public async Task<Character> GetCharacterAsync(HttpClient httpClient, string id)
        {
            var character = await httpClient.GetFromJsonAsync<Character>($"https://rickandmortyapi.com/api/character/{id}");
            return character;
        }
    }
}
