using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITSadok.DotNetMentorship.Admin.Data;
using ITSadok.DotNetMentorship.Admin.Data.Entity;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;
using Microsoft.AspNetCore.JsonPatch;
using ITSadok.DotNetMentorship.Admin.API.Commands;
using ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;
using ITSadok.DotNetMentorship.Admin.API.Commands.StudentCommands;

namespace ITSadok.DotNetMentorship.Admin.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly UnitOfWork _unitOfWork;

        Switch s = new Switch();

        public StudentController(UnitOfWork UnitOfWork, ILogger<StudentController> logger)
        {
            _unitOfWork = UnitOfWork;
            _logger = logger;
        }

        // GET: api/student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            ICommandWithResult<List<StudentDTO>> command = new GetAllStudentsCommand(_unitOfWork);
            await s.StoreAndExecute(command);
            return Ok(command.Result);
        }

        // GET: api/Student/{studentId}
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDTO>> GetStudent(int id)
        {
            ICommandWithResult<StudentDTO> command = new GetStudentByIdCommand(_unitOfWork, id);
            await s.StoreAndExecute(command);
            return Ok(command.Result);
        }

        // PATCH: api/Student/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchStudent(int id, JsonPatchDocument StudentJSON)
        {
            ICommand command = new PatchStudentCommand(_unitOfWork, id, StudentJSON);
            await s.StoreAndExecute(command);
            return Ok();
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromBody]Student student)
        {
            ICommandWithResult<StudentDTO> command = new CreateStudentCommand
                (_unitOfWork, student, _logger);
            await s.StoreAndExecute(command);
            return CreatedAtAction("GetStudent", new { id = student.Id }, 
                command.Result);
        }
    }
}
