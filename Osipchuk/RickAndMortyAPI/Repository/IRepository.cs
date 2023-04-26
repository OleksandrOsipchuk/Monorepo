using RickMorty;

namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T>
    {
        IAsyncEnumerable<T> GetCharactersAsync();
        Task<T> GetCharacterAsync(int id);
        Task CreateAsync(T item);
        Task CreateManyAsync(IList<T> items);
        Task<T> DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task UpdateManyAsync(IList<Character> items);
        Task Save();
        void ClearTracker();
    }
}
