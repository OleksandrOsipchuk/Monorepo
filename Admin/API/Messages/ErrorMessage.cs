using Admin.API.Messages.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Admin.API.Messages
{
    public class ErrorMessage : TgMessage
    {
        public readonly TelegramBotClient _TgClient;
        public readonly Update _update;
        public ErrorMessage(TelegramBotClient TgClient, Update update) : base(TgClient, update)
        {
            this._TgClient = TgClient;
            this._update = update;
        }

        internal async Task<bool> SendError()
        {
            switch (_update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    await _TgClient.SendTextMessageAsync(
                    chatId: _update.Message.From.Id,
                    text: "Something went wrong!");
                    return true;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    await _TgClient.SendTextMessageAsync(
                   chatId: _update.CallbackQuery.From.Id,
                   text: "Something went wrong!");
                    return true;
            }
            return false;
        }
    }
}
