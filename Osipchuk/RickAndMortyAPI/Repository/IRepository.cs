using RickMorty;

namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T>
    {
        IAsyncEnumerable<T> GetCharactersAsync();
        Task<T> GetCharacterAsync(int id);
        Task CreateAsync(T item);
        Task CreateAsync(IEnumerable<T> items);
        Task<T> DeleteAsync(int id);
        Task UpdateAsync(T item);
        Task UpdateAsync(IEnumerable<Character> items);
        Task Save();
        void ClearTracker();
    }
}
