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
            Console.WriteLine($"Stage: {UserStage}");
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
                    unitOfWork: _unitOfWork,
                    userStage: UserStage
                    );

                // Starting command
                if (Message.Text == "/start")
                {
                    UserStage = "default";
                    await TgMethods.Start();
                    return Ok();
                }
                
                // For Subscription renewal
                if (UserStage != null && UserStage.Contains("SubscriptionRenewal_"))
                {
                    string NewStage = await TgMethods.RenewSubAsync();
                    if (NewStage != UserStage) UserStage = NewStage;
                }

                // For User Registration
                if (UserStage != null && UserStage.Contains("UserRegistration_"))
                {
                    string NewStage = await TgMethods.RegisterUserAsync();
                    if (NewStage != UserStage) UserStage = NewStage;
                }

                // Case if message came from group
                if (Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group && Message.Text == "/registergroup")
                {
                    GroupId = await TgMethods.RegisterGroupAsync();
                    return Ok();
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
                    unitOfWork: _unitOfWork,
                    userStage: UserStage
                    );


                // Register from CallBack
                if (update.CallbackQuery.Data == "/register_")
                {
                    TgMethods.userStage = await TgMethods.RegisterUserAsync();
                    UserStage = TgMethods.userStage;
                }
                // Check if user can take access to callback actions (He does, if he is registered)
                bool IsAccessed = await TgMethods.CheckUsersAccessToCallbackAsync();
                if (!IsAccessed) return Ok();

                // MentionStudentsAsync function (ADMIN ONLY)
                if (update.CallbackQuery.Data.Contains("/mention"))
                {
                    await TgMethods.MentionStudentsAsync();
                }
                // Back to /start menu
                if (update.CallbackQuery.Data == "/return_")
                {
                    UserStage = "default";
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
                
                // Renew user's subscription by admin
                if (update.CallbackQuery.Data == "/renew_" ^ update.CallbackQuery.Data.Contains("renewal_"))
                {
                    TgMethods.userStage = await TgMethods.RenewSubAsync();
                    UserStage = TgMethods.userStage;
                }
            }
            return Ok();
        }
    }
}
