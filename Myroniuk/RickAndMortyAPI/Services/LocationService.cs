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
