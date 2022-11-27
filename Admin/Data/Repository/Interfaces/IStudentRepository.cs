using Admin.Data.Entity;
using Admin.Data.Repository.Interfaces;

namespace Admin.Data.Repository.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetByTelegramIdAsync(long TelegramIdIn);
    }
}
