namespace RickAndMortyAPI.Repository
{
    public class UnitOfWork : IDisposable
    {
        private RickAndMortyContext db = new RickAndMortyContext();
        private RickAndMortyRepository repository;      
        public RickAndMortyRepository Repository
        {
            get 
            {
                if(repository == null)
                {
                    repository = new RickAndMortyRepository(db);
                }
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
                    db.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
