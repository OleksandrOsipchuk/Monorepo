using Microsoft.AspNetCore.Mvc;
using SuperCompany.Domain;

namespace SuperCompany.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DataManager _dataManager;
        public ServicesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public IActionResult Index(Guid id)
        {
            if (id != default)
            {
                var service = _dataManager.serviceItemRepository.GetServiceItemById(id);
                return View("Show",service);
            }
            ViewBag.TextFild = _dataManager.textFildRepository.GetTextFild("PageServices");
            return View(_dataManager.serviceItemRepository.GetServiceItems());
        }
    }
}
