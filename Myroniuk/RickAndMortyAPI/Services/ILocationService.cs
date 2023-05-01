using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetLocationsAsync();
        Task<IEnumerable<LocationDTO>> GetLocationsAsync(int[] entityIDs);
    }
}
