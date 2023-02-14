using ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;
using ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;
using ITSadok.DotNetMentorship.Admin.Data.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Razor.Language.Extensions;

namespace ITSadok.DotNetMentorship.Admin.API.Commands.StudentCommands;

public class PatchStudentCommand : ICommand
{
    private readonly UnitOfWork _unitOfWork;
    private readonly int _id;
    private readonly JsonPatchDocument _jsonPatchDocument;

    public PatchStudentCommand(UnitOfWork unitOfWork, int id, JsonPatchDocument StudentJSON)
    {
        _unitOfWork = unitOfWork;
        _id = id;
        _jsonPatchDocument = StudentJSON;
    }

    public async Task Execute()
    {
        await _unitOfWork.StudentRepository.PatchAsync(_id, _jsonPatchDocument);
        await _unitOfWork.SaveAsync();
    }
}