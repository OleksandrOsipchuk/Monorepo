using ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;
using ITSadok.DotNetMentorship.Admin.API.Controllers;
using ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using ITSadok.DotNetMentorship.Admin.Data.Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Security.Cryptography;

namespace ITSadok.DotNetMentorship.Admin.API.Commands.StudentCommands;

public class GetStudentByIdCommand : ICommandWithResult<StudentDTO>
{
    private readonly UnitOfWork _unitOfWork;
    private readonly int _id;
    public StudentDTO Result { get; set; }

    public GetStudentByIdCommand(UnitOfWork unitOfWork, int StudentId)
    {
        _id = StudentId;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var student = await _unitOfWork.StudentRepository.GetByIdAsync(_id);

        if (student == null)
        {
            Result = null;
        }
        var studentDTO = new StudentDTO();

        Result = studentDTO.TransformToDTO(student);
    }
}