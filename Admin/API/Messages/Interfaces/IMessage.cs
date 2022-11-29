using Telegram.Bot;
using Telegram.Bot.Types;

namespace Admin.API.Messages.Interfaces
{
    public interface IMessage
    {
        internal Task<bool> SendAsync(string text);
    }
}
