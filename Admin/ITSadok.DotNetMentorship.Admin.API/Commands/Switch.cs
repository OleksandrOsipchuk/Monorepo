using ITSadok.DotNetMentorship.Admin.API.Commands.Interfaces;

namespace ITSadok.DotNetMentorship.Admin.API.Commands;

public class Switch
{
    private List<ICommand> _commands = new List<ICommand>();

    public async Task StoreAndExecute(ICommand command)
    {
        _commands.Add(command);
        await command.Execute();
    }
}