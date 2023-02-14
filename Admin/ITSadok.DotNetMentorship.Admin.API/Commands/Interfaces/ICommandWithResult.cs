namespace ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;

public interface ICommandWithResult<T> : ICommand
{
    T Result { get; set; }
}
