using ITSadok.DotNetMentorship.Admin.Data.Entity.DTO;

namespace ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;

public interface ICommand
{
    Task Execute();
}

