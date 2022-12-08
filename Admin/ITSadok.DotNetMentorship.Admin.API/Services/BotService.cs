using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace ITSadok.DotNetMentorship.Admin.API.Services
{
    public class BotService
    {
        public ITelegramBotClient Bot { get; }

        public BotService(IOptions<BotSettings> botOptions)
        {
            Bot = new TelegramBotClient(botOptions.Value.TelegramApiKey);
        }
    }
}
