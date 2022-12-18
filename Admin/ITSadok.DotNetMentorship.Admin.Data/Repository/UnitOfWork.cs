using System;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;

namespace ITSadok.DotNetMentorship.Admin.Data.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        public StudentRepository StudentRepository { get; set; }
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            StudentRepository = new StudentRepository(_context);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
