namespace RickAndMortyAPI.Entities
{
    public class APIResponse
    {
            public Info Info { get; set; }
            public IReadOnlyCollection<Location> Results { get; set; }
    }
}
