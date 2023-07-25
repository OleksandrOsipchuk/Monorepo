using SuperCompany.Domain.Repositories;

namespace SuperCompany.Domain
{
    public class DataManager
    {
        public ITextFildRepository textFildRepository { get; set; }
        public IServiceItemRepository serviceItemRepository { get; set; }
        public DataManager(ITextFildRepository textFildRepository, IServiceItemRepository serviceItemRepository)
        {
            this.serviceItemRepository = serviceItemRepository;
            this.textFildRepository= textFildRepository;
        }
    }
}
