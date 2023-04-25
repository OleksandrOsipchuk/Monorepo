using RickMorty;

namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T>
    {
        IAsyncEnumerable<T>  GetCharactersAsync();
        Task<T> GetCharacterAsync(int id);
        Task<T> DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task CreateAsync(T item);
        Task CreateManyAsync(IList<T> items);
        Task Save();
    }
}
