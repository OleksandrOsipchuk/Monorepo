using Admin.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Admin.Data.Repository.Interfaces;

namespace Admin.Data.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Student> GetByTelegramIdAsync(long TelegramIdIn)
        {
            return await _context.Students.Where(b => b.TelegramId == TelegramIdIn).FirstOrDefaultAsync<Student>();
        }

    }
}