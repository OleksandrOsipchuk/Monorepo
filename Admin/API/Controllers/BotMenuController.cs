using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgAdmin.Controllers;
using Admin.Data.Repository.Interfaces;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using System.Collections.Generic;
using System;
using Admin.Data.JSON;
using System.Text.Json;
using Admin.Data.Entity;
using Admin.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Admin.API.Messages;
using Admin.API.Controllers.InlineKeyboards;
using System.Security.Cryptography.X509Certificates;

namespace TgModerator.API.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotMenuController : ControllerBase
    {
        private static string _TelegramAPIKey = "5492109358:AAHzQpHb2PRz9KeLedC0Z59OHJehbhkdHeo";
        public static long GroupId;
        public static AppInlineKeyboards InlineKeyboards = new AppInlineKeyboards();
        public InlineKeyboardMarkup InlineKeyboardMenu = InlineKeyboards.InlineKeyboardMenu;
        public InlineKeyboardMarkup InlineKeyboardManageMenu = InlineKeyboards.InlineKeyboardManageMenu;
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

            // Case if update is received as message
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) 
            {
                User = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(update.Message.From.Id);

                // Case if message came from chat
                if (update.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group && update.Message.Text == "/registergroup")
                {
                    if (User.isAdmin)
                    {
                        GroupId = update.Message.Chat.Id;
                        JsonData Data = new JsonData("MainGroupId", GroupId);
                        string jsonString = System.Text.Json.JsonSerializer.Serialize(Data);

                        // Saving ChatID to JSON
                        string fileName = "ApplicationData.json";
                        using FileStream createStream = System.IO.File.Create(fileName);
                        await System.Text.Json.JsonSerializer.SerializeAsync(createStream, Data);
                        await createStream.DisposeAsync();

                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: "You have registered this group as main group.");
                    }
                    else
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: "You must be admin.");
                    }
                    return Ok();
                }

                // Starting command
                if (update.Message.Text == "/start")
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                        chatId: update.Message.From.Id,
                        text: "Welcome to the ItSadok Moderation bot!",
                        replyMarkup: InlineKeyboardMenu);
                }

                // Subscription renewal (ADMIN ONLY)
                if (User != null && User.isAdmin && update.Message.Text.Contains("/renew"))
                {
                    string[] TextArray = update.Message.Text.Split(" ");
                    string RenewalType = TextArray[1];
                    long UserTelegramId = Int32.Parse(TextArray[2]);
                    string InputDate = TextArray[3];
                    DateTime DateTimeNow = DateTime.Now;
                    Message sentMessage;
                    Student UserToUpdate = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
                    if (UserToUpdate == null)
                    {
                        await ErrorMessage.Send(_TgClient, update);
                        return Ok();
                    }
                    if (RenewalType.Equals("0"))
                    {
                        Subscription UserSubscription = await _unitOfWork.SubscriptionRepository.GetByStudentIdAsync(User.Id);
                        if (UserSubscription.ExpirationDate == DateTime.MinValue)
                        {
                            try 
                            { 
                                UserSubscription.ExpirationDate = DateTimeNow.Date.AddMonths(Int32.Parse(InputDate));
                                
                                if (UserSubscription.ExpirationDate > DateTime.Now)
                                {
                                    UserSubscription.IsExpired = false;
                                }
                            }
                            catch { return Ok(); }
                        }
                        else
                        {
                            try 
                            { 
                                UserSubscription.ExpirationDate = UserSubscription.ExpirationDate.AddMonths(Int32.Parse(InputDate));
                                
                                if (UserSubscription.ExpirationDate > DateTime.Now)
                                {
                                    UserSubscription.IsExpired = false;
                                }
                            }
                            catch
                            {
                                await ErrorMessage.Send(_TgClient, update);
                                return Ok();
                            }
                        }
                    }
                    else if (RenewalType.Equals("1"))
                    {
                        try
                        {
                            Subscription UserSubscription = await _unitOfWork.SubscriptionRepository.GetByStudentIdAsync(User.Id);
                            UserSubscription.ExpirationDate = DateTime.Parse(InputDate);
                            if (UserSubscription.ExpirationDate > DateTime.Now)
                            {
                                UserSubscription.IsExpired = false;
                            }
                            _unitOfWork.Save();
                        }
                        catch
                        {
                            await ErrorMessage.Send(_TgClient, update);
                            return Ok();
                        }

                    }
                    _unitOfWork.StudentRepository.UpdateAsync(UserToUpdate);
                    _unitOfWork.Save();
                    sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.From.Id,
                            text: $"User {UserToUpdate.Name} DateTimeNow has subscription until:\n{UserToUpdate.Subscription.ExpirationDate}");
                }

                // User registration
                if (update.Message.Text.Contains("/reg"))
                {
                    if (User == null)
                    {
                        User = new Student();
                        Subscription UserSubscription = new Subscription();
                        UserSubscription.StudentForeignKey = User.Id;

                        _unitOfWork.StudentRepository.InsertAsync(User);
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
                            User.StudentLevel = 0;

                            Subscription UserSubscription = new Subscription();
                            UserSubscription.ExpirationDate = DateTime.MinValue;
                            UserSubscription.IsExpired = true;
                            User.Subscription = UserSubscription;

                            _unitOfWork.StudentRepository.UpdateAsync(User);
                            _unitOfWork.Save();

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

                // Set level for user (ADMIN ONLY)
                if (update.Message.Text.Contains("/setlevel"))
                {
                    if (User != null && User.isAdmin)
                    {
                        string[] TextArray = update.Message.Text.Split(" ");
                        long UserTelegramId = Int64.Parse(TextArray[1]);
                        int NewLevel = Int32.Parse(TextArray[2]);
                        if (NewLevel != 1 && NewLevel != 2)
                        {
                            await ErrorMessage.Send(_TgClient, update);
                            return Ok();
                        }
                        Student UserToUpdate = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
                        UserToUpdate.StudentLevel = NewLevel;
                        _unitOfWork.StudentRepository.UpdateAsync(UserToUpdate);
                        _unitOfWork.Save();
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                                chatId: update.Message.From.Id,
                                text: $"Updated!\nName: {UserToUpdate.Name}\nLevel: {UserToUpdate.StudentLevel}");
                    }
                }

                //
            }

            // Case if update is received as callback
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                User = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(update.CallbackQuery.From.Id);

                // Check if user can take access to callback actions (He does, if he is registered)
                if (User == null ^ !User.isRegistered)
                { 
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                    chatId: update.CallbackQuery.From.Id,
                    text: "Please, register in this system in such way:" +
                    "\n/reg {Name} {Surname} {Occupation (Student/Working)} {GitHub link}");
                    
                    return Ok();
                }

                // Register from CallBack
                if (update.CallbackQuery.Data.ToString() == "/register_")
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "You are already registered.");
                }

                // Transfer to appinlinekeyboards
                var Students = await _unitOfWork.StudentRepository.GetAllAsync();
                List<String> StudentNames = new List<string>();
                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (Student s in Students)
                {
                    StudentNames.Add(s.Name);
                    InlineKeyboardButton StudentButton = InlineKeyboardButton
                        .WithCallbackData(text: $"{s.Name}", callbackData: $"{s.TelegramId} student_");
                    buttons.Add(StudentButton);
                }
                InlineKeyboardMarkup InlineKeyboardStudentsList = new InlineKeyboardMarkup(buttons);

                // Mention function (ADMIN ONLY)
                if (update.CallbackQuery.Data.Contains("/mention"))
                {
                    int MentionLevel;
                    
                    // Mention specific user level
                    if (update.CallbackQuery.Data.Contains("level_"))
                    {
                        MentionLevel = Int32.Parse(update.CallbackQuery.Data.Split(" ")[2]);
                        var students = Students.Where(s => s.StudentLevel == MentionLevel);
                        List<String> TelegramTags = new List<string>();
                        foreach (Student s in students) 
                            TelegramTags.Add("@" + s.TelegramUserName);
                        string Tags = string.Join(", ", TelegramTags);
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                                chatId: GroupId,
                                text: $"Alert!\n" +
                                $"{Tags}");
                    }

                    // Mention all
                    else if (update.CallbackQuery.Data.Contains("all_"))
                    {
                        List<String> TelegramTags = new List<string>();
                        foreach (Student s in Students)
                            TelegramTags.Add("@" + s.TelegramUserName);
                        string Tags = string.Join(", ", TelegramTags);
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                                chatId: GroupId,
                                text: $"Alert!\n" +
                                $"{Tags}");
                    }
                }
                // Back to /start menu
                if (update.CallbackQuery.Data == "/return_")
                {
                    Message sentMessage = await _TgClient.EditMessageReplyMarkupAsync(
                        chatId: update.CallbackQuery.From.Id,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: InlineKeyboardMenu);
                }

                // Get info about chosen student (ADMIN ONLY)
                if (update.CallbackQuery.Data.Contains("student_"))
                {
                    long UserTelegramId = Int64.Parse(update.CallbackQuery.Data.Split(" ")[0]);
                    Student UserAbout = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
                    string UserInfo = UserAbout.GetSubscriptionInfo();
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: UserInfo);
                }

                // Subscription check 
                if (update.CallbackQuery.Data == "/subscriptionCheck_")
                {
                    if (User == null)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "You must register first!");
                        return Ok();
                    }

                    // If user is admin, print keyboard with all student to choose by click
                    if (User.isAdmin)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "Check",
                            replyMarkup: InlineKeyboardStudentsList);
                    }

                    //If user is not admin, print info about him
                    else
                    {
                        string Info = User.GetSubscriptionInfo();
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: Info);
                    }
                }

                // Manage button 
                if (update.CallbackQuery.Data == "/manage_")
                {
                    if (User == null)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "You must register first and obtain admin rules!");
                    }
                    else if (User != null && !User.isAdmin)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: "You must obtain admin rules!");
                    }

                    // If user is registered and is admin
                    else
                    {
                        Message sentMessage = await _TgClient.EditMessageReplyMarkupAsync(
                           chatId: update.CallbackQuery.From.Id,
                           messageId: update.CallbackQuery.Message.MessageId,
                           replyMarkup: InlineKeyboardManageMenu);
                    }
                }

                // How to set level
                if (update.CallbackQuery.Data == "/setlevel_")
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: "To set student's level, enter command:" +
                           "\n/setlevel {User's telegram id} {Student's level: 1/2}");
                }

                // Print info about all students  (ADMIN ONLY)
                if (update.CallbackQuery.Data == "/studentslist_")
                {
                    List<string> OutputDataList = new List<string>();
                    foreach (Student s in Students)
                    {
                        string text = s.GetInfo();
                        OutputDataList.Add(text);
                    }
                    string OutputData = string.Join(" ", OutputDataList.ToArray());
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: OutputData);
                }
                
                if (update.CallbackQuery.Data == "/renew_")
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: "To renew student's subscription, enter command:" +
                           "\nTo add an amount of months(from today if user never had a subscription or from the expiration date)" +
                           "\n/renew 0 {User's telegram id} {Amount of months to add}" +
                           "\nOR to set a specific date:" +
                           "\n/renew 1 {User's telegram id} {Date in format DD.MM.YYYY}");
                }
            }


            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
            }
            //TO DELETE
            //Grant admin access (DELETE IN FUTURE)
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Text == "/adminme")
            {

                if (User != null && !User.isAdmin)
                {
                    User.isAdmin = true;

                    _unitOfWork.StudentRepository.UpdateAsync(User);
                    _unitOfWork.Save();
                }
                else
                {
                    await ErrorMessage.Send(_TgClient, update);
                }
            }
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Text == "/delete")
            {
                _unitOfWork.StudentRepository.DeleteAsync(User);
                _unitOfWork.Save();
            }
            //TO DELETE

            return Ok();
        }
    }

}
