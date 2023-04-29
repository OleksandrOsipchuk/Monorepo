using RickAndMortyAPI.CharacterInfo;
using RickMorty;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Services
{
    public interface IPullCharactersJob
    {
        Task RunAsync();
    }
}
