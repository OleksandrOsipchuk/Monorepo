using Microsoft.AspNetCore.Mvc;
using SuperCompany.Domain.Entities;
using SuperCompany.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using SuperCompany.Models;
using Microsoft.Extensions.Hosting.Internal;

namespace SuperCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceItemsController : Controller
    {
        private readonly IServiceItemRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ServiceItemsController(IServiceItemRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var serviceItem = id == default ? new ServiceItem() : _repository.GetServiceItemById(id);
            return View(serviceItem);
        }
        [HttpPost]
        public IActionResult Edit(ServiceItem model, IFormFile titleImageFile)
        {
            if (ModelState.IsValid)
            {
                if (titleImageFile != null) 
                {
                    model.TitleImagePath = titleImageFile.FileName;
                    using (var stream = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "images/", titleImageFile.FileName), FileMode.Create))
                    {
                        titleImageFile.CopyTo(stream);
                    }
                }
                _repository.SeveServiceItem(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                _repository.DeleteServiceItem(id);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
