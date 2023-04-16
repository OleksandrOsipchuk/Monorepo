using RickAndMortyAPI.Middleware;

namespace RickAndMortyAPI.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly RickAndMortyContext _dbcontext;
        private LocationRepository _repository;
        public UnitOfWork(RickAndMortyContext dbcontext) {
            _dbcontext = dbcontext;
        }
        public LocationRepository Repository { 
            get { _repository ??= new LocationRepository(_dbcontext);  return _repository; }
        }
        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbcontext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
