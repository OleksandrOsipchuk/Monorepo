using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RickAndMortyAPI.Entities;
using RickAndMortyAPI.Middleware;

namespace RickAndMortyAPI.Repository
{
    public class LocationRepository : IRepository<Location>
    {
        private readonly RickAndMortyContext _dbcontext;
        public LocationRepository(RickAndMortyContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var locations = _dbcontext.Locations;
            return await locations.ToListAsync();
        }
        public async Task<IEnumerable<Location>> GetByIdAsync(int[] entityIDs)
        {
            var locations = _dbcontext.Locations.Where(l => entityIDs.Contains(l.Id));
            return await locations.ToListAsync();
        }
        public void Insert(Location entity)
        {
            _dbcontext.Locations.Add(entity);
        }
        public void Save()
        {
            _dbcontext.SaveChanges();
        }
        public void Update(Location entity)
        {
            _dbcontext.Entry(entity).State = EntityState.Modified;
        }
        public async Task<Location?> DeleteAsync(int id)
        {
            var location = await _dbcontext.Locations.FindAsync(id);
            if (location != null)
            {
                _dbcontext.Remove(location);
            }
            return location;
        }
    }
}
