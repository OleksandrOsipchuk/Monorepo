using Admin.API.Messages.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;

namespace Admin.API.Messages
{
    public class TgMessage : IMessage
    {
        public readonly TelegramBotClient _TgClient;
        public readonly Update _update;

        public TgMessage(TelegramBotClient TgClient, Update update)
        {
            this._TgClient = TgClient;
            this._update = update;
        }
        public async Task<bool> SendAsync(string text)
        {
            switch (_update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    await _TgClient.SendTextMessageAsync(
                    chatId: _update.Message.From.Id,
                    text: text);
                    return true;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    await _TgClient.SendTextMessageAsync(
                   chatId: _update.CallbackQuery.From.Id,
                   text: text);
                    return true;
            }
            return false;
        }

        public async Task<bool> SendWithReplyMarkupAsync(string text, InlineKeyboardMarkup InlineKeyboard)
        {
            switch (_update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    await _TgClient.SendTextMessageAsync(
                        chatId: _update.Message.From.Id,
                        text: text,
                        replyMarkup: InlineKeyboard);
                    return true;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    await _TgClient.SendTextMessageAsync(
                   chatId: _update.CallbackQuery.From.Id,
                   text: text,
                   replyMarkup: InlineKeyboard);
                    return true;
            }
            return false;
        }

        public async Task<bool> EditMessageReplyMarkupAsync(InlineKeyboardMarkup InlineKeyboard)
        {
            switch (_update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    
                    return false;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    Message sentMessage = await _TgClient.EditMessageReplyMarkupAsync(
                    chatId: _update.CallbackQuery.From.Id,
                        messageId: _update.CallbackQuery.Message.MessageId,
                        replyMarkup: InlineKeyboard);
                    return true;
            }
            return false;
        }
        public async Task<bool> SendToGroupAsync(string text, long GroupId)
        {
            switch (_update.Type)
            {
                case Telegram.Bot.Types.Enums.UpdateType.Message:
                    await _TgClient.SendTextMessageAsync(
                    chatId: GroupId,
                    text: text);
                    return true;

                case Telegram.Bot.Types.Enums.UpdateType.CallbackQuery:
                    await _TgClient.SendTextMessageAsync(
                   chatId: GroupId,
                   text: text);
                    return true;
            }
            return false;
        }
    }
}
