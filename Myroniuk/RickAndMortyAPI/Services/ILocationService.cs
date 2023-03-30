using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Services
{
    public interface ILocationService
    {
        /// <summary>
        /// Returns an asynchronous enumerable of all locations from the Rick and Morty API.
        /// </summary>
        /// <returns>An asynchronous enumerable of <c>Location</c> objects.</returns>
        IAsyncEnumerable<Location> GetLocationsAsync();
        /// <summary>
        /// Retrieves an asynchronous enumerable of locations with the specified IDs from the Rick and Morty API.
        /// </summary>
        /// <param name="ids">The IDs of the locations to retrieve.</param>
        /// <returns>An asynchronous enumerable of <c>Location</c> objects representing the locations with the specified IDs.</returns>
        IAsyncEnumerable<Location> GetLocationsAsync(string ids);
    }
}
