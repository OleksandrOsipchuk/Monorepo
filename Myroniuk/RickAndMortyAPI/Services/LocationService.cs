using RickAndMortyAPI.Entities;
using Newtonsoft.Json;
using RickAndMortyAPI.Repository;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace RickAndMortyAPI.Services
{
    public class LocationService : ILocationService
    {
        private LocationRepository _repository;
        public LocationService(UnitOfWork unitOfWork) { 
            _repository = unitOfWork.Repository;
        }
        /// <summary>
        /// Retrieves an asynchronous enumerable of all locations.
        /// </summary>
        /// <returns>An asynchronous enumerable of <c>Location</c> objects.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        public async IAsyncEnumerable<LocationDTO> GetLocationsAsync() {
           await foreach (var location in _repository.GetAllAsync()) {
                yield return new LocationDTO
                {
                    Id = location.Id,
                    Name = location.Name,
                    Type = location.Type,
                    Dimension = location.Dimension
                };
            }
        }
        /// <summary>
        /// Retrieves an asynchronous enumerable of locations with the specified IDs.
        /// </summary>
        /// <param name="ids">The IDs of the locations to retrieve.</param>
        /// <returns>An asynchronous enumerable of <c>Location</c> objects representing the locations with the specified IDs.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        public async IAsyncEnumerable<LocationDTO> GetLocationsAsync(int[] entityIDs) {
            await foreach (var location in _repository.GetByIdAsync(entityIDs))
            {
                yield return new LocationDTO
                {
                    Id = location.Id,
                    Name = location.Name,
                    Type = location.Type,
                    Dimension = location.Dimension
                };
            }

        }
    }
}
