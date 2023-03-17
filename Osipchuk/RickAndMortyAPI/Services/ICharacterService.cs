using RickMorty;

namespace RickAndMortyAPI
{
    public interface ICharacterService
    {
        Task<Character> GetCharacterAsync(HttpClient httpClient, string id);
        IAsyncEnumerable<Character> GetAllCharactersAsync(HttpClient httpClient);
    }
}
