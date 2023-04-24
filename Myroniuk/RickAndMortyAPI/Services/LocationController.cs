using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RickAndMortyAPI.Entities;
using System.Xml.Linq;

namespace RickAndMortyAPI.Services
{
    [ApiController]
    [Route("api/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }
        [HttpGet]
        [EndpointSummary("Get all locations.")]
        [Tags("Locations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Produces("application/json")]
        [Route("api/locations")]
        public async Task<IActionResult> GetLocationsAsync()
        {
            var locations = new List<LocationDTO>();
            await foreach (var location in _locationService.GetLocationsAsync())
            {
                locations.Add(location);
            }
            return Ok(locations);
        }

        [HttpGet]
        [EndpointSummary("Get locations by id.")]
        [Tags("Locations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Produces("application/json")]
        [Route("api/location")]
        public async Task<IActionResult> GetLocationsByIdAsync([FromQuery(Name = "locationIDs")] int[] locationIDs)
        {
            var locations = new List<LocationDTO>();
            await foreach (var location in _locationService.GetLocationsAsync(locationIDs))
            {
                locations.Add(location);
            }
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }
    }
}
