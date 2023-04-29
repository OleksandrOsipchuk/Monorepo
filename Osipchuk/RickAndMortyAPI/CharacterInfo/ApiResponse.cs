using RickMorty;

namespace RickAndMortyAPI.CharacterInfo
{
    public class ApiResponse
    {
        public Info Info { get; set; }
        public List<CharacterAPIResponse> Results { get; set; }
    }
}
