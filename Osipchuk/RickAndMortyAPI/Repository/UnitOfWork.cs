namespace RickAndMortyAPI.Repository
{
    public class UnitOfWork : IDisposable
    {
        private RickAndMortyContext db = new RickAndMortyContext();
        private RickAndMortyRepository repository;      
        public RickAndMortyRepository Characters
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
        public void Save()
        {
            db.SaveChanges();
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
