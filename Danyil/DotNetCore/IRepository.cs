using System.Linq.Expressions;

namespace DotNetMentorship.TestAPI
{
    public interface IRepository : IDisposable
    {
        
        Task<IEnumerable<Ukrainian>> GetAllAsync();
        Task<Ukrainian> GetByIdAsync(int id);
        Task<Ukrainian> AddAsync(Ukrainian user);
        Task<Ukrainian> DeleteAsync(int id);
        Task<Ukrainian> UpdateAsync(int id, Ukrainian user);
        Task Save();
    }
}