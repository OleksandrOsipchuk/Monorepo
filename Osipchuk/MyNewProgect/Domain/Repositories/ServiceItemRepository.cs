using Microsoft.EntityFrameworkCore;
using SuperCompany.Domain.Entities;
using System.Linq;

namespace SuperCompany.Domain.Repositories
{
    public class ServiceItemRepository : IServiceItemRepository
    {
        private AppDbContext _dbContext;
        public ServiceItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ServiceItem GetServiceItemById(Guid id)
        {
            var serviceItems = _dbContext.ServiceItems.FirstOrDefault(x => x.Id == id);
            if (serviceItems != null)
            {
                return serviceItems;
            }
            else throw new NullReferenceException();
        }
        public IQueryable<ServiceItem> GetServiceItems()
        {
            return _dbContext.ServiceItems;
        }
        public void DeleteServiceItem(Guid id)
        {
            var serviceItem = _dbContext.ServiceItems.FirstOrDefault(x => x.Id == id);
            if (serviceItem != null)
            {
                _dbContext.ServiceItems.Remove(serviceItem);
                _dbContext.SaveChanges();
            }
            else throw new NullReferenceException();
        }
        public void SeveServiceItem(ServiceItem serviceItem)
        {
            if (serviceItem.Id == default)
                _dbContext.Entry(serviceItem).State = EntityState.Added;
            else
                _dbContext.Entry(serviceItem).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
