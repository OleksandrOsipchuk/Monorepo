using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Services
{
    public interface ILocationService
    {
        IAsyncEnumerable<LocationDTO> GetLocationsAsync();
        IAsyncEnumerable<LocationDTO> GetLocationsAsync(int[] entityIDs);
    }
}
