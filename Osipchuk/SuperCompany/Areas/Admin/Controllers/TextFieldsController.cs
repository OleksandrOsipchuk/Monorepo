using Microsoft.AspNetCore.Mvc;
using SuperCompany.Domain.Entities;
using SuperCompany.Domain.Repositories;

namespace SuperCompany.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TextFieldsController : Controller
    {
        private readonly ITextFildRepository _repository;
        public TextFieldsController(ITextFildRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult Edit(string CodeWord)
        {
            var fild = _repository.GetTextFild(CodeWord);
            return View(fild);
        }
        [HttpPost]
        public IActionResult Edit(TextFild model)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveTextFild(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
