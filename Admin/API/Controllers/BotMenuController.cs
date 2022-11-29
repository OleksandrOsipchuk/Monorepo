using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Admin.Data.JSON;
using Admin.Data.Entity;
using Admin.Data.Repository;
using Admin.API.Messages;
using Admin.API.Controllers.InlineKeyboards;
using Admin.API.Methods;
using Admin.Data.Repository.Interfaces;
using System.Collections.Generic;

namespace TgModerator.API.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotMenuController : ControllerBase
    {
        public static string UserStage;
        private static string _TelegramAPIKey = "";
        public static long GroupId;
        
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<BotMenuController> _logger;
        public BotMenuController(ILogger<BotMenuController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _unitOfWork = new UnitOfWork(dbContext);
        }
        private TelegramBotClient _TgClient = new TelegramBotClient(_TelegramAPIKey);

        [HttpPost]
        public async Task <IActionResult> Post([FromBody] Update update)
        {
            
            Telegram.Bot.Types.Enums.UpdateType UpdateType = update.Type;
            Student User = new Student();
            TgMessage Msg = new TgMessage(_TgClient, update);

            // Case if update is received as message
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) 
            {
                var Message = update.Message;
                User = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(Message.From.Id);

                TelegramMethods TgMethods = new TelegramMethods(
                    user: User,
                    tgMessage: Msg,
                    update: update,
                    unitOfWork: _unitOfWork
                    );

                // Case if message came from group
                if (Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group && Message.Text == "/registergroup")
                {
                    GroupId = await TgMethods.RegisterGroupAsync();
                    return Ok();
                }

                // Starting command
                if (Message.Text == "/start")
                {
                    await TgMethods.Start();
                }

                // Subscription renewal (ADMIN ONLY)
                if (User != null && User.isAdmin && Message.Text.Contains("/renew"))
                {
                    await TgMethods.RenewSubscriptionAsync();
                }

                // User registration
                if (update.Message.Text.Contains("/reg"))
                {
                    await TgMethods.RegisterUser();
                }

                // Set level for user (ADMIN ONLY)
                if (update.Message.Text.Contains("/setlevel"))
                {
                    await TgMethods.SetUserLevelAsync();
                }

                //TO DELETE
                //Grant admin access (DELETE IN FUTURE)
                if (update.Message.Text == "/adminme")
                {
                    await TgMethods.GrantAdminAccess();
                }
                if (update.Message.Text == "/delete")
                {
                    _unitOfWork.StudentRepository.DeleteAsync(User);
                    _unitOfWork.Save();
                }
                //TO DELETE
            }

            // Case if update is received as callback
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                User = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(update.CallbackQuery.From.Id);

                TelegramMethods TgMethods = new TelegramMethods(
                    user: User,
                    tgMessage: Msg,
                    update: update,
                    unitOfWork: _unitOfWork
                    );

                // Check if user can take access to callback actions (He does, if he is registered)
                bool IsAccessed = await TgMethods.CheckUsersAccessToCallbackAsync();
                if (!IsAccessed) return Ok();

                
                // Register from CallBack
                if (update.CallbackQuery.Data == "/register_")
                {
                    bool isRegistered = await TgMethods.CheckUsersRegistrationAsync();
                    if (!isRegistered) return Ok();
                }

                // MentionStudentsAsync function (ADMIN ONLY)
                if (update.CallbackQuery.Data.Contains("/mention"))
                {
                    await TgMethods.MentionStudentsAsync();
                }
                // Back to /start menu
                if (update.CallbackQuery.Data == "/return_")
                {
                    TgMethods.ReturnToStartKeyboardAsync();
                }

                // Get info about chosen student (ADMIN ONLY)
                if (update.CallbackQuery.Data.Contains("student_"))
                {
                    await TgMethods.PrintInfoAboutStudentAsync();
                    
                }

                // Subscription check 
                if (update.CallbackQuery.Data == "/subscriptionCheck_")
                {
                    await TgMethods.CheckUserSubscriptionAsync();
                }

                // Manage button 
                if (update.CallbackQuery.Data == "/manage_")
                {
                    TgMethods.ManageAsync();
                }

                // How to set level
                if (update.CallbackQuery.Data == "/setlevel_")
                {
                    TgMethods.HowToSetLevel();
                }

                // Print info about all students  (ADMIN ONLY)
                if (update.CallbackQuery.Data == "/studentslist_")
                {
                    await TgMethods.ListAllSudents();
                }
                
                if (update.CallbackQuery.Data == "/renew_")
                {
                    TgMethods.HowToRenew();
                }
            }`
            return Ok();
        }
    }

}
