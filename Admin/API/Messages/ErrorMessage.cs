using Telegram.Bot;
using Telegram.Bot.Types;

namespace Admin.API.Messages
{
    public class ErrorMessage
    {
        public static async Task<bool> Send(TelegramBotClient TgClient, Update update)
        {
            try
            {
                await TgClient.SendTextMessageAsync(
                    chatId: update.Message.From.Id,
                    text: "Something went wrong!");
                return true;
            }
            catch { }
            try
            {
                await TgClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.From.Id,
                    text: "Something went wrong!");
                return true;
            }
            catch { }
            return false;
        }
    }
}
