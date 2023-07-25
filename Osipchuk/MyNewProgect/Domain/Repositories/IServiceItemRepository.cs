using SuperCompany.Domain.Entities;

namespace SuperCompany.Domain.Repositories
{
    public interface IServiceItemRepository
    {
        IQueryable<ServiceItem> GetServiceItems();
        ServiceItem GetServiceItemById(Guid id);
        void SeveServiceItem(ServiceItem serviceItem);
        void DeleteServiceItem(Guid id);

    }
}
