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

namespace TgModerator.API.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotMenuController : ControllerBase
    {
        private static string _TelegramAPIKey = "5492109358:AAHzQpHb2PRz9KeLedC0Z59OHJehbhkdHeo";
        public static long GroupId;

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
            Student User = new Student();
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message) 
            {
                User = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(update.Message.From.Id);
            }
            else
            {
                User = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(update.CallbackQuery.From.Id);
            }
            

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message &&
                update.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group)
            {
                if (update.Message.Text == "/registergroup")
                {
                    if (User.isAdmin)
                    {
                        GroupId = update.Message.Chat.Id;

                        JsonData Data = new JsonData("MainGroupId", GroupId);
                        string jsonString = System.Text.Json.JsonSerializer.Serialize(Data);

                        string fileName = "ApplicationData.json";
                        using FileStream createStream = System.IO.File.Create(fileName);
                        await System.Text.Json.JsonSerializer.SerializeAsync(createStream, Data);
                        await createStream.DisposeAsync();

                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: "You have registered this group as main group.");
                    }
                }

                return Ok();
            }

            if ((update.Type == Telegram.Bot.Types.Enums.UpdateType.Message && update.Message.Text == "/start"))
            {
                
                Message sentMessage = await _TgClient.SendTextMessageAsync(
                    chatId: update.Message.From.Id,
                    text: "Welcome to the ItSadok Moderation bot!",
                    replyMarkup: InlineKeyboardMenu);
            }

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


            //Registration
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.CallbackQuery.Data.ToString() == "/register_")
            {
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

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {

                if (update.Message.Text.Contains("/check") && User.isAdmin)
                {
                    string[] TextArray = update.Message.Text.Split(" ");

                }
                if (User != null && update.Message.Text.Contains("/renew") && User.isAdmin)
                {
                    string[] TextArray = update.Message.Text.Split(" ");
                    string RenewalType = TextArray[1];
                    long UserTelegramId = Int32.Parse(TextArray[2]);
                    string InputDate = TextArray[3];
                    DateTime now = DateTime.Now;
                    Message sentMessage;
                    Student UserToUpdate = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
                    if (UserToUpdate == null)
                        {
                        await ErrorMessage.Send(_TgClient, update);
                    }
                    else if (RenewalType.Equals("0"))
                    {
                        Subscription UserSubscription = await _unitOfWork.SubscriptionRepository.GetByStudentIdAsync(User.Id);
                        if (UserSubscription.ExpirationDate == DateTime.MinValue)
                        {
                            try { UserSubscription.ExpirationDate = now.Date.AddMonths(Int32.Parse(InputDate)); }
                            catch { return Ok(); }
                        }
                        else
                        {
                            try { UserSubscription.ExpirationDate = UserSubscription.ExpirationDate.AddMonths(Int32.Parse(InputDate)); } 
                            catch {
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
                            _unitOfWork.Save();
                        }
                        catch { await ErrorMessage.Send(_TgClient, update);
                            return Ok();
                        }
                        
                    }
                    _unitOfWork.StudentRepository.UpdateAsync(UserToUpdate);
                    _unitOfWork.Save();
                    sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.From.Id,
                            text: $"User {UserToUpdate.Name} now has subscription until:\n{UserToUpdate.Subscription.ExpirationDate}");
                }

                //Registration
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
            }
            //Manage
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
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
                

                if (update.CallbackQuery.Data.Contains("/mention"))
                {
                    int LevelToMention;
                    if (update.CallbackQuery.Data.Contains("level_"))
                    {
                        LevelToMention = Int32.Parse(update.CallbackQuery.Data.Split(" ")[2]);
                        var students = Students.Where(s => s.StudentLevel == LevelToMention);
                        List<String> TelegramTags = new List<string>();
                        foreach (Student s in students)
                        {
                            TelegramTags.Add("@" + s.TelegramUserName);
                        }
                        string Tags = string.Join(", ", TelegramTags);
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                                chatId: GroupId,
                                text: $"Alert!\n" +
                                $"{Tags}");
                    }
                    else if (update.CallbackQuery.Data.Contains("all_"))
                    {
                        
                        List<String> TelegramTags = new List<string>();
                        foreach (Student s in Students)
                        {
                            TelegramTags.Add("@" + s.TelegramUserName);
                        }
                        string Tags = string.Join(", ", TelegramTags);
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                                chatId: GroupId,
                                text: $"Alert!\n" +
                                $"{Tags}");
                    }
                }

                if (update.CallbackQuery.Data == "/return_")
                {
                    Message sentMessage = await _TgClient.EditMessageReplyMarkupAsync(
                        chatId: update.CallbackQuery.From.Id,
                        messageId: update.CallbackQuery.Message.MessageId,
                        replyMarkup: InlineKeyboardMenu);
                }

                if (update.CallbackQuery.Data.Contains("student_"))
                {
                    long UserTelegramId = Int64.Parse(update.CallbackQuery.Data.Split(" ")[0]);
                    Student UserAbout = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
                    string UserInfo = UserAbout.GetSubscriptionInfo();
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: UserInfo);

                }

                if (update.CallbackQuery.Data == "/subscriptionCheck_")
                {
                    if (User == null)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "You must register first!");
                        return Ok();
                    }
                    if (User.isAdmin)
                    {
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: "Check",
                            replyMarkup: InlineKeyboardStudentsList);
                    }
                    else
                    {
                        string Info = User.GetSubscriptionInfo();
                        Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: Info);
                    }
                }

                if (update.CallbackQuery.Data == "/manage_")
                {
                    Student user = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(update.CallbackQuery.From.Id);
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
                        Message sentMessage = await _TgClient.EditMessageReplyMarkupAsync(
                           chatId: update.CallbackQuery.From.Id,
                           messageId: update.CallbackQuery.Message.MessageId,
                           replyMarkup: InlineKeyboardManageMenu);
                    }
                }
                
                if (update.CallbackQuery.Data == "/setlevel_")
                {
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                           chatId: update.CallbackQuery.From.Id,
                           text: "To set student's level, enter command:" +
                           "\n/setlevel {User's telegram id} {Student's level: 1/2}");
                }

                if (update.CallbackQuery.Data == "/studentslist_")
                {
                    List<string> OutputDataList = new List<string>();
                    
                    foreach (Student s in Students)
                    {
                        string text = s.GetInfo();
                        OutputDataList.Add(text);
                    }
                    string OutputData = string.Join(" ",OutputDataList.ToArray());
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
            return Ok();
        }
    }

}
