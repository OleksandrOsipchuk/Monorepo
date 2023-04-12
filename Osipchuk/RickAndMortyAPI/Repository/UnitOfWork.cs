namespace RickAndMortyAPI.Repository
{
    public class UnitOfWork : IDisposable
    {
        private readonly RickAndMortyContext _db;
        private RickAndMortyRepository? repository;
        public UnitOfWork(RickAndMortyContext db)
        {
            _db = db;
        }

        public RickAndMortyRepository Repository
        {
            get
            {
                repository ??= new RickAndMortyRepository(_db);
                return repository;
            }
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
