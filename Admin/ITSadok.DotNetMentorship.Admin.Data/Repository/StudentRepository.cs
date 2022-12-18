using ITSadok.DotNetMentorship.Admin.Data.Entity;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSadok.DotNetMentorship.Admin.Data.Repository
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
            var students = GetAllAsync();
            return await Filter(
                (IQueryable<Student>)students,
                b => b.TelegramUser.TelegramId == TelegramIdIn)
                .FirstOrDefaultAsync<Student>();
        }

    }
}
