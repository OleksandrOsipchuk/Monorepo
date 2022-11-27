namespace Admin.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        void InsertAsync(T obj);
        void UpdateAsync(T obj);
        void DeleteAsync(T obj);
    }
}
