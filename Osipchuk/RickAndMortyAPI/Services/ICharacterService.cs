using RickMorty;

namespace RickAndMortyAPI
{
    public interface ICharacterService
    {
        Task<Character> GetCharacterAsync(int id);
        IAsyncEnumerable<Character> GetAllCharactersAsync();
    }
}
