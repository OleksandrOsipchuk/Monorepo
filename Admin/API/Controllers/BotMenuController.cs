using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgAdmin.Controllers;
using TgModerator.Data.Repository.IRepository;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using TgModerator.Data.Entity;

namespace TgModerator.API.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotMenuController : ControllerBase
    {
        public InlineKeyboardMarkup InlineKeyboardMenu = new(new[]
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

        public InlineKeyboardMarkup InlineKeyboardManageMenu = new(new[]
     {
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "List all students", callbackData: "/studentslist_"),
            InlineKeyboardButton.WithCallbackData(text: "Renew the subscription", callbackData: "/renew_"),
            InlineKeyboardButton.WithCallbackData(text: "Check subscription", callbackData: "/subscriptionCheck_"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Mention level 1", callbackData: "/mention1level_"),
            InlineKeyboardButton.WithCallbackData(text: "Mention level 2", callbackData: "/mention2level_"),
            InlineKeyboardButton.WithCallbackData(text: "Mention all students", callbackData: "/mentionall_"),
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData(text: "Return", callbackData: "/start_"),
        },
    });

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BotMenuController> _logger;
        public bool IsAdminIn { get; set; }
        public long RegisteringId{ get; set; }
        public BotMenuController(ILogger<BotMenuController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        private TelegramBotClient _TgClient = new TelegramBotClient("");

        [HttpPost]
        public async Task <IActionResult> Post([FromBody] Update update)
        {

            //Start menu
            if ((update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Text == "/start")
                ^ (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.CallbackQuery.Data == "/start_"))
            {
                
                Message sentMessage = await _TgClient.SendTextMessageAsync(
                    chatId: update.Message.From.Id,
                    text: "Welcome to the ItSadok Moderation bot!",
                    replyMarkup: InlineKeyboardMenu);
            }

            //Grant admin access (DELETE IN FUTURE)
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Text == "/adminme")
            {
                Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.Message.From.Id);
                if (User != null && !User.isAdmin)
                {
                    User.isAdmin = true;
                    _unitOfWork.Student.Update(User);
                }
                else
                {
                    Message sentmessage = await _TgClient.SendTextMessageAsync(
                        chatId: update.Message.From.Id,
                        text: "Something went wrong!");
                }
            }

            //Registration

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.CallbackQuery.Data.ToString() == "/register_")
            {
                Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.CallbackQuery.From.Id);
                if (User != null && User.isRegistered)
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                        chatId: update.CallbackQuery.From.Id,
                        text: "You are already registered.");
                }
                else
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.From.Id,
                    text: "Please, register in this system in such way:" +
                    "\n/reg {Name} {Surname} {Occupation (Student/Working)} {GitHub link}");
                }

            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Text.Contains("/reg"))
            {
                Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.Message.From.Id);
                if (User == null)
                {
                    User = new Student();
                    await _unitOfWork.Student.InsertAsync(User);
                    _unitOfWork.Save();
                }
                
                if (User.isRegistered)
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                        chatId: update.Message.From.Id,
                        text: "You are already registered.");
                }
                else
                {
                    try
                    {
                        string[] TextArray = update.Message.Text.Split(" ");

                        User.Name = TextArray[1];
                        User.Surname = TextArray[2];
                        User.Occupation = TextArray[3];
                        User.GithubLink = TextArray[4];
                        User.TelegramId = update.Message.From.Id;
                        User.TelegramUserName = update.Message.From.Username;
                        User.isRegistered = true;
                        _unitOfWork.Student.Update(User);

                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                        chatId: update.Message.From.Id,
                        text: "You have been succesfully registered!",
                        replyMarkup: InlineKeyboardMenu);
                        }
                    catch (Exception ex)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.From.Id,
                            text: ex.Message);
                        throw;
                    }
                }
                
            }
            //Manage
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                if (update.CallbackQuery.Data == "/manage_")
                {
                    Student user = await _unitOfWork.Student.GetByTelegramIdAsync(update.CallbackQuery.From.Id);
                    if (user == null)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "You must register first and obtain admin rules!");
                    }
                    else if (user != null && !user.isAdmin)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: "You must obtain admin rules!");
                    }
                    else
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: "Welcome back, admin =)",
                           replyMarkup: InlineKeyboardManageMenu);
                    }
                }
            }

            return Ok();
        }
    }
}
