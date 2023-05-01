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
        public async Task<IEnumerable<LocationDTO>> GetLocationsAsync() {
            var locations = await _repository.GetAllAsync();
            return locations.Select(location => new LocationDTO
            {
                Id = location.Id,
                Name = location.Name,
                Type = location.Type,
                Dimension = location.Dimension
            });
        }
        public async Task<IEnumerable<LocationDTO>> GetLocationsAsync(int[] entityIDs) {
            var locations = await _repository.GetByIdAsync(entityIDs);
            return locations.Select(location => new LocationDTO
            {
                Id = location.Id,
                Name = location.Name,
                Type = location.Type,
                Dimension = location.Dimension
            });
        }
    }
}
