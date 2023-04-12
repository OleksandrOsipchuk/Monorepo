namespace RickAndMortyAPI.Services
{
    public interface ICharacterService<T>
    {
        public Task<T> GetDTOAsync(int id);
        public IAsyncEnumerable<T> GetDTOsAsync();
    }
}
