using Admin.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Admin.Data.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
    where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties).ToList();
        }
        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            TEntity? entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async void InsertAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
        }
        public async void UpdateAsync(TEntity item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
        }
        public async void DeleteAsync(TEntity item)
        {
            _dbSet.Remove(item);
        }
    }
}