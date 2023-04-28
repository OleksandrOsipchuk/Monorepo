using RickAndMortyAPI.CharacterInfo;
using RickMorty;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Services
{
    public interface IPullCharactersJob
    {
        Task RunAsync();
        protected IAsyncEnumerable<IEnumerable<Character>> GetCharactersAsync();
        protected IEnumerable<Character> ConvertFromCharacterAPI(List<CharacterAPIResponse> apiCharacters);
    }
}
