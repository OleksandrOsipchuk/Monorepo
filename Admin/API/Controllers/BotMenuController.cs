using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgAdmin.Controllers;
using TgModerator.Data.Repository.IRepository;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading;
using TgModerator.Data.Entity;
using System.Collections.Generic;
using System;
using Admin.API.Controllers;
using Admin.Data.JSON;
using System.Text.Json;

namespace TgModerator.API.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class BotMenuController : ControllerBase
    {
        private static string _TelegramAPIKey = "";
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

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BotMenuController> _logger;
        public BotMenuController(ILogger<BotMenuController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        private TelegramBotClient _TgClient = new TelegramBotClient(_TelegramAPIKey);

        [HttpPost]
        public async Task <IActionResult> Post([FromBody] Update update)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message &&
                update.Message.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Group)
            {
                if (update.Message.Text == "/registergroup")
                {
                    Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.Message.From.Id);
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
            //^(update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery && update.CallbackQuery.Data == "/start_")
            //Start menu
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
                Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.Message.From.Id);
                if (User != null && !User.isAdmin)
                {
                    User.isAdmin = true;
                    _unitOfWork.Student.Update(User);
                }
                else
                {
                    await ErrorMessage.Send(_TgClient, update);
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

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.Message.From.Id);

                

                if (update.Message.Text.Contains("/check") && User.isAdmin)
                {
                    string[] TextArray = update.Message.Text.Split(" ");

                }
                if (update.Message.Text.Contains("/renew") && User.isAdmin)
                {
                    string[] TextArray = update.Message.Text.Split(" ");
                    string RenewalType = TextArray[1];
                    long UserTelegramId = Int32.Parse(TextArray[2]);
                    string InputDate = TextArray[3];
                    DateTime now = DateTime.Now;
                    Message sentMessage;
                    Student UserToUpdate = await _unitOfWork.Student.GetByTelegramIdAsync(UserTelegramId);
                    if (UserToUpdate == null)
                        {
                        await ErrorMessage.Send(_TgClient, update);
                    }
                    else if (RenewalType.Equals("0"))
                    {
                        if (UserToUpdate.SubscriptionUntil == DateTime.MinValue)
                        {
                            try { UserToUpdate.SubscriptionUntil = now.Date.AddMonths(Int32.Parse(InputDate)); }
                            catch { return Ok(); }
                        }
                        else
                        {
                            try { UserToUpdate.SubscriptionUntil = UserToUpdate.SubscriptionUntil.AddMonths(Int32.Parse(InputDate)); } 
                            catch { ErrorMessage.Send(_TgClient, update);
                                return Ok();
                            }
                        }
                    }
                    else if (RenewalType.Equals("1"))
                    {
                        try { UserToUpdate.SubscriptionUntil = DateTime.Parse(InputDate); }
                        catch { ErrorMessage.Send(_TgClient, update);
                            return Ok();
                        }
                        
                    }
                    _unitOfWork.Student.Update(UserToUpdate);
                    _unitOfWork.Save();
                    sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.Message.From.Id,
                            text: $"User {UserToUpdate.Name} now has subscription until:\n{UserToUpdate.SubscriptionUntil}");
                }

                //Registration
                if (update.Message.Text.Contains("/reg"))
            {
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
                            User.StudentLevel = 0;
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
                if (update.Message.Text.Contains("/setlevel"))
                {
                    if (User != null && User.isAdmin)
                    {
                        string[] TextArray = update.Message.Text.Split(" ");
                        long UserTelegramId = Int64.Parse(TextArray[1]);
                        int NewLevel = Int32.Parse(TextArray[2]);
                        if (NewLevel != 1 && NewLevel != 2)
                        {
                            ErrorMessage.Send(_TgClient, update);
                            return Ok();
                        }
                        Student UserToUpdate = await _unitOfWork.Student.GetByTelegramIdAsync(UserTelegramId);
                        UserToUpdate.StudentLevel = NewLevel;
                        _unitOfWork.Student.Update(UserToUpdate);
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
                Student User = await _unitOfWork.Student.GetByTelegramIdAsync(update.CallbackQuery.From.Id);

                var Students = await _unitOfWork.Student.GetAsync();
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
                    Student UserAbout = await _unitOfWork.Student.GetByTelegramIdAsync(UserTelegramId);
                    string UserInfo = UserAbout.GetSubscriptionInfo();
                    Message sentMessage = await _TgClient.SendTextMessageAsync(
                            chatId: update.CallbackQuery.From.Id,
                            text: UserInfo);

                }

                if (update.CallbackQuery.Data == "/subscriptionCheck_")
                {
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
