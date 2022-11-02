using DotNetMentorship.TestAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace DotNetMentorship.TestAPI
{
    public class UnitOfWork : IDisposable
    {
        private UkrainianDbContext _DbContext;
        private GenericRepository<Ukrainian> _UkrainianRepository;

        public UnitOfWork(UkrainianDbContext context)
        {
            this._DbContext = context;
        }
        public GenericRepository<Ukrainian> UkrainianRepository
        {
            get
            {

                if (this._UkrainianRepository == null)
                {
                    this._UkrainianRepository = new GenericRepository<Ukrainian>(_DbContext);
                }
                return _UkrainianRepository;
            }
        }


        public void Save()
        {
            _DbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
