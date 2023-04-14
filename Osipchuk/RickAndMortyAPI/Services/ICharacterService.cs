using RickAndMortyAPI.CharacterInfo;

namespace RickAndMortyAPI.Services
{
    public interface ICharacterService
    {
        public Task<CharacterDTO> GetCharacterAsync(int id);
        public IAsyncEnumerable<CharacterDTO> GetCharactersAsync();
    }
}
