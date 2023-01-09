using ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;
using ITSadok.DotNetMentorship.Admin.API.Controllers;
using ITSadok.DotNetMentorship.Admin.Data.Entity;
using ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace ITSadok.DotNetMentorship.Admin.API.Commands.StudentCommands;

public class CreateStudentCommand : ICommandWithResult<StudentDTO   >
{
    private readonly UnitOfWork _unitOfWork;
    private readonly Student _student;
    private readonly ILogger<StudentController> _logger;
    public StudentDTO Result { get; set; }


    public CreateStudentCommand(UnitOfWork unitOfWork, Student student, ILogger<StudentController> logger)
    {
        _unitOfWork = unitOfWork;
        _student = student;
        _logger = logger;
    }

    public async Task Execute()
    {
        Subscription userSubscription = new Subscription();
        TelegramUser telegramUser = new TelegramUser();
        userSubscription.StudentId = _student.Id;
        telegramUser.StudentId = _student.Id;
        _student.Subscription = userSubscription;
        _student.TelegramUser = telegramUser;
        try
        {
            _unitOfWork.StudentRepository.InsertAsync(_student);
            _logger.LogInformation("Created user with name {0}", _student.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }

        await _unitOfWork.SaveAsync();

        var studentDTO = new StudentDTO();
        Result = studentDTO.TransformToDTO(_student);

    }
}