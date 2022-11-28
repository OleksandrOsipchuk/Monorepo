using Telegram.Bot.Types.ReplyMarkups;

namespace Admin.API.Controllers.InlineKeyboards
{
    public class AppInlineKeyboards
    {
        public InlineKeyboardMarkup InlineKeyboardMenu;
        public InlineKeyboardMarkup InlineKeyboardManageMenu;
        public InlineKeyboardMarkup InlineKeyboardStudentList;
        public AppInlineKeyboards()
        {
            InlineKeyboardMenu = new(new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Register", callbackData: "/register_"),
            InlineKeyboardButton.WithCallbackData(text: "Manage", callbackData: "/manage_"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Check subscription", callbackData: "/subscriptionCheck_"),
            InlineKeyboardButton.WithCallbackData(text: "Get Information", callbackData: "/getInfo_"),
        },
    });
            InlineKeyboardManageMenu = new(new[]
     {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "List all students", callbackData: "/studentslist_"),
            InlineKeyboardButton.WithCallbackData(text: "Renew the subscription", callbackData: "/renew_"),
            InlineKeyboardButton.WithCallbackData(text: "Check subscription", callbackData: "/subscriptionCheck_"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Mention level 1", callbackData: "/mention level_ 1"),
            InlineKeyboardButton.WithCallbackData(text: "Mention level 2", callbackData: "/mention level_ 2"),
            InlineKeyboardButton.WithCallbackData(text: "Mention all students", callbackData: "/mention all_"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Set a level", callbackData: "/setlevel_"),
            InlineKeyboardButton.WithCallbackData(text: "Return", callbackData: "/return_"),
        },
    });
        }
    }
}
