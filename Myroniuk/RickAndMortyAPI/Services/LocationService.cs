using RickAndMortyAPI.Entities;
using Newtonsoft.Json;

namespace RickAndMortyAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlAPI;
        public LocationService(HttpClient httpClient, IConfiguration configuration) { 
            _httpClient = httpClient;
            _urlAPI = configuration.GetSection("urlAPI").Value;
        }
        /// <summary>
        /// Retrieves an asynchronous enumerable of all locations from the Rick and Morty API.
        /// </summary>
        /// <returns>An asynchronous enumerable of <c>Location</c> objects.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        public async IAsyncEnumerable<Location> GetLocationsAsync()
        {
            var pageUrl = _urlAPI;
            while(!string.IsNullOrEmpty(pageUrl))
            {
                var response = await _httpClient.GetAsync(pageUrl);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<APIResponse>(responseContent);
                pageUrl = result.Info.Next;
                foreach(var location in result.Results)
                    yield return location;
            }
        }
        /// <summary>
        /// Retrieves an asynchronous enumerable of locations with the specified IDs from the Rick and Morty API.
        /// </summary>
        /// <param name="ids">The IDs of the locations to retrieve.</param>
        /// <returns>An asynchronous enumerable of <c>Location</c> objects representing the locations with the specified IDs.</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="JsonException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        public async IAsyncEnumerable<Location> GetLocationsAsync(string id)
        { 
            foreach (var locationId in id.Split(',').Select(x => x.Trim()).ToArray()) 
                yield return await _httpClient.GetFromJsonAsync<Location>($"{_urlAPI}/{locationId}");
        }
    }
}
