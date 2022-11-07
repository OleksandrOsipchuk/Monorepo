using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetMentorship.TestAPI.Repository
{
    public class Repository : IRepository, IDisposable
    {
        private UkrainianDbContext _RepoContext;
        public Repository(UkrainianDbContext context)
        {
            var _RepoContext = context;
        }


        public async Task<Ukrainian> GetByIdAsync(int id)
        {
            return await _RepoContext.Ukrainians.FindAsync(id);
        }
        public async Task<IEnumerable<Ukrainian>> GetAllAsync()
        {
            try
            {
                return await _RepoContext.Ukrainians.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("{Repo} All function error ", typeof(Repository), $"\n{{Repo}}{ex.Message}");
                return new List<Ukrainian>();
            }
        }

        public async Task<Ukrainian> AddAsync(Ukrainian user)
        {
            try
            {
                await _RepoContext.Ukrainians.AddAsync(user);
                await _RepoContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{Repo} Create function error", typeof(Repository), $"{{Repo}} {ex.Message}");
                return null;
            }
        }

        public async Task<Ukrainian> UpdateAsync(int id, Ukrainian user)
        {
            try
            {
                var existingUser = await _RepoContext.Ukrainians.Where(x => x.Id == id)
                                                    .FirstOrDefaultAsync();
                    
                    existingUser.Name = user.Name;
                    existingUser.City = user.City;
                    existingUser.IsCalm = user.IsCalm;
                    await _RepoContext.SaveChangesAsync();
                    
                    return existingUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{Repo} Update function error", typeof(Repository), $"{{Repo}} {ex.Message}");
                return null;
            }
        }
        

        public async Task<Ukrainian> DeleteAsync(int id)
        {
            try
            {
                var user = await _RepoContext.Ukrainians.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception();
                }

                _RepoContext.Ukrainians.Remove(user);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{Repo} Delete function error", typeof(Repository), $"{{Repo}} {ex.Message}");
                return null;
            }
        }

        public async Task Save()
        {
            await _RepoContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _RepoContext.Dispose();
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
