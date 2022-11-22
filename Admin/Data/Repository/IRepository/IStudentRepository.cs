using TgModerator.Data.Entity;

namespace TgModerator.Data.Repository.IRepository
{
    public interface IStudentRepository : IDisposable
    {
        Task<IEnumerable<Student>> GetAsync();
        Task<Student> GetByIDAsync(int StudentId);
        Task<Student> GetByTelegramIdAsync(long TelegramId);
        Task InsertAsync(Student student);
        Task DeleteAsync(int studentID);
        void Update(Student student);
    }
}
