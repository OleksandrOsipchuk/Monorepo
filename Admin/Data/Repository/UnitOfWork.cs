using Microsoft.EntityFrameworkCore.Metadata;
using TgModerator.Data.Repository.IRepository;

namespace TgModerator.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentContext _context;

        public UnitOfWork(StudentContext context)
        {
            _context = context;
            Student = new StudentRepository(_context);
        }

        public StudentRepository Student { get; set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
