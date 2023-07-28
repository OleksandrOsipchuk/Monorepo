using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuperCompany.Domain;

namespace SuperCompany.Models.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly DataManager _dataManager;

        public SidebarViewComponent(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public Task<IViewComponentResult> InvokeAsync()
        {
            return Task.FromResult((IViewComponentResult) View("Default", _dataManager.serviceItemRepository.GetServiceItems()));
        }
    }
}
