using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Admin.Data.Repository.Interfaces;

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
            var students = _unitOfWork.StudentRepository.GetWithInclude(s => s.Subscription);
            return Ok(students);
        }
        [HttpGet]
        [Route("/short")]
        public async Task<IActionResult> IndexTest()
        {
            var students = await _unitOfWork.StudentRepository.GetAllAsync();
            return Ok(students);
        }

        [HttpGet]
        [Route("/subs")]
        public async Task<IActionResult> IndexSubs()
        {
            var subscriptions = await _unitOfWork.SubscriptionRepository.GetAllAsync();
            return Ok(subscriptions);
        }
    }
}
