namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T>
    {
        IAsyncEnumerable<T>  GetCharactersAsync();
        Task<T> GetCharacterAsync(int id);
        Task<T> DeleteAsync(int id);
        void Update(T item);
        void CreateAsync(T item);
        void Save();
    }
}
