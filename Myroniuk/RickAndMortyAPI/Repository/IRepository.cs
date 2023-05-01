using RickAndMortyAPI.Entities;

namespace RickAndMortyAPI.Repository
{
    public interface IRepository<T> where T : class
    {
        //considering ActionResult class
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByIdAsync (int[] entityIDs);
        void Insert(T entity);
        void Update(T entity);
        void Save();
        Task<T?> DeleteAsync(int id);
    }
}
