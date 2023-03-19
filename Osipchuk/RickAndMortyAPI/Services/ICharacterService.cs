using RickMorty;

namespace RickAndMortyAPI
{
    public interface ICharacterService
    {
        Task<Character> GetCharacterAsync(string id);
        IAsyncEnumerable<Character> GetAllCharactersAsync();
    }
}
