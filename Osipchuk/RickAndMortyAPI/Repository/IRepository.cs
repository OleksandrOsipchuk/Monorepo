namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T,D>
    {
        IAsyncEnumerable<D>  GetCharactersAsync();
        Task<D> GetCharacterAsync(int id);
        Task<T> DeleteAsync(int id);
        void Update(T item);
        void CreateAsync(T item);
    }
}
