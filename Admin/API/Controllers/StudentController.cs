using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TgModerator.Data.Entity;
using TgModerator.Data.Repository.IRepository;

namespace TgAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StudentController> _logger;
        public StudentController(ILogger<StudentController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            
            /*Student user1 = new Student { Name = "Tom" };
            await _unitOfWork.Student.InsertAsync(user1);
            _unitOfWork.Save();*/
            var students = await _unitOfWork.Student.GetAsync();
            return Ok(students);
        }


       
    }
}
