using ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;
using ITSadok.DotNetMentorship.Admin.API.Controllers;
using ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ITSadok.DotNetMentorship.Admin.API.Commands.StudentCommands;

public class GetAllStudentsCommand : ICommandWithResult<List<StudentDTO>>
{
    private readonly UnitOfWork _unitOfWork;
    public List<StudentDTO> Result { get; set; }


    public GetAllStudentsCommand(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var students = await _unitOfWork.StudentRepository.GetAllAsync();
        var studentsDTO = new StudentDTO();
        var studentsDTOList = studentsDTO.TransformToDTOList(students);
        Result = (List<StudentDTO>)studentsDTOList;
    }
}