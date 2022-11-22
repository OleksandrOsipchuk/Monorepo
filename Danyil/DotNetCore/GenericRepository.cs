using DotNetMentorship.TestAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;

namespace DotNetMentorship.TestAPI
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal UkrainianDbContext DbContext;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(UkrainianDbContext context)
        {
            this.DbContext = context;
            this.DbSet = context.Set<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async virtual Task<TEntity> GetByIDAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public async virtual void InsertAsync(TEntity entity)
        {
            await DbContext.AddAsync(entity);
        }

        public async virtual Task DeleteAsync(object id)
        {
            TEntity entityToDelete = await DbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbContext.Remove(entityToDelete);
        }

        public async virtual void Update(int id, TEntity entity)
        {
            this.Delete(await GetByIDAsync(id));
            this.InsertAsync(entity);
        }
    }
}
