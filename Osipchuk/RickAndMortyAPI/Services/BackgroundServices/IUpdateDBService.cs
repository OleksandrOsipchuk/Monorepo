using RickMorty;
using System.Threading.Tasks;

namespace RickAndMortyAPI.Services
{
    public interface IUpdateDBService
    {
        Task UpdateDBAsync();
        Task<IList<Character>> GetCharactersAsync();        
    }
}
