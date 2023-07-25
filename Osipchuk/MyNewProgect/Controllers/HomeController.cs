using Microsoft.AspNetCore.Mvc;
using SuperCompany.Domain;
using System.Diagnostics;
using WebApplication1.Models;

namespace SuperCompany.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataManager _dataManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            var textPage = _dataManager.textFildRepository.GetTextFild("PageIndex");
            return View(textPage);
        }

        public IActionResult Contacts()
        {
            var textPage = _dataManager.textFildRepository.GetTextFild("PageContacts");
            return View(textPage);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
