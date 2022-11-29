using Admin.API.Messages;
using Admin.API.Controllers.InlineKeyboards;
using Admin.Data.Entity;
using Admin.Data.Repository;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Admin.API.Methods
{
    public class TelegramMethods
    {
        private Student user;
        private readonly TgMessage _message;
        private readonly Update _update;
        private readonly UnitOfWork _unitOfWork;
        public string userStage;

        public static AppInlineKeyboards InlineKeyboards = new AppInlineKeyboards();
        public InlineKeyboardMarkup InlineKeyboardMenu = InlineKeyboards.InlineKeyboardMenu;
        public InlineKeyboardMarkup InlineKeyboardManageMenu = InlineKeyboards.InlineKeyboardManageMenu;

        public TelegramMethods(Student user, TgMessage tgMessage, Update update, UnitOfWork unitOfWork, string userStage)
        {
            this.user = user;
            _message = tgMessage;
            _update = update;
            _unitOfWork = unitOfWork;
            this.userStage = userStage;
        }

        // TODO: change type to Task<bool> and fix GroupId save to JSON
        public async Task<long> RegisterGroupAsync()
        {
            if (user.isAdmin)
            {
                long GroupId = _update.Message.Chat.Id;
                await _message.SendToGroupAsync(text: "You have registered this group as main group.", GroupId: GroupId);
                return GroupId;
            }
            else
            {
                await _message.SendAsync($"You must be admin. {user.isAdmin} {user.Name}");
                return 0;
            }
        }

        public async Task<bool> Start()
        {
            await _message.SendWithReplyMarkupAsync("Welcome to the ItSadok Moderation bot!", InlineKeyboardMenu);
            return true;
        }

        public async Task<string> RenewSubAsync()
        {
            if (userStage ==  null ^ userStage == "default")
            {
                userStage = "SubscriptionRenewal_1_0";
            }
            if (userStage.Split("_")[0] == "SubscriptionRenewal")
            {
                int SubscriptionRenewalStage = Int32.Parse(userStage.Split("_")[1]);
                long UserToUpdateTgId = Int64.Parse(userStage.Split("_")[2]);
                switch (SubscriptionRenewalStage)
                {
                    case 1:
                        await _message.SendWithReplyMarkupAsync(text: "Choose student to renew subscription",
                        InlineKeyboard: await GenerateStudentsListKeyboardAsync("renewal"));
                            return $"SubscriptionRenewal_{SubscriptionRenewalStage + 1}_0";
                    case 2:
                        long StudentTgId = Int64.Parse(_update.CallbackQuery.Data.Split(" ")[0]);
                        await _message.SendAsync(text: "Enter date to set as an expiration date.");
                        return $"SubscriptionRenewal_{SubscriptionRenewalStage + 1}_{StudentTgId}";
                    case 3:
                        if (UserToUpdateTgId == 0)
                        {
                            await _message.SendErrorAsync();
                            return (userStage);
                        }
                        Student UserToUpdate = await _unitOfWork
                            .StudentRepository.GetByTelegramIdAsync(UserToUpdateTgId);
                        DateTime InputDate = DateTime.Now;
                        try { InputDate = DateTime.Parse(_update.Message.Text); } 
                        catch { 
                            await _message.SendErrorAsync();
                            return userStage;
                        }
                        if (UserToUpdate == null)
                        {
                            await _message.SendErrorAsync();
                        }
                        else
                        {
                            Subscription UserSubscription = await _unitOfWork
                                .SubscriptionRepository.GetByStudentIdAsync(UserToUpdate.Id);
                            UserSubscription.ExpirationDate = InputDate;
                            if (InputDate >= DateTime.Now) UserSubscription.IsExpired = false;
                            _unitOfWork.Save();

                            await _message.SendAsync($"You successfully renewed susbcription for " +
                                $"user {UserToUpdate.Name}\n" + UserToUpdate.GetInfo(UserSubscription));
                            return "default";
                        }
                        break;
                }
            }
            return "default";
        }

        public async Task<bool> RegisterUser()
        {
            if (user == null)
            {
                user = new Student();
                Subscription UserSubscription = new Subscription();
                UserSubscription.StudentForeignKey = user.Id;

                _unitOfWork.StudentRepository.InsertAsync(user);
                _unitOfWork.Save();
                
            }

            if (user.isRegistered)
            {
                await _message.SendAsync(text: "You are already registered.");
                return true;
            }
            else
            {
                try
                {
                    string[] TextArray = _update.Message.Text.Split(" ");

                    user.Name = TextArray[1];
                    user.Surname = TextArray[2];
                    user.Occupation = TextArray[3];
                    user.GithubLink = TextArray[4];
                    user.TelegramId = _update.Message.From.Id;
                    user.TelegramUserName = _update.Message.From.Username;
                    user.isRegistered = true;
                    user.StudentLevel = 0;

                    Subscription UserSubscription = new Subscription();
                    UserSubscription.ExpirationDate = DateTime.MinValue;
                    UserSubscription.IsExpired = true;
                    user.Subscription = UserSubscription;

                    _unitOfWork.StudentRepository.UpdateAsync(user);
                    _unitOfWork.Save();

                    await _message.SendWithReplyMarkupAsync(text: "You have been succesfully registered!", InlineKeyboard: InlineKeyboardMenu);
                    return true;
                }
                catch 
                {
                    await _message.SendErrorAsync();
                    return false;
                }
            }
        }

        public async Task<bool> SetUserLevelAsync()
        {
            if (user != null && user.isAdmin)
            {
                string[] TextArray = _update.Message.Text.Split(" ");
                long UserTelegramId = Int64.Parse(TextArray[1]);
                int NewLevel = Int32.Parse(TextArray[2]);
                if (NewLevel != 1 && NewLevel != 2)
                {
                    await _message.SendErrorAsync();
                    return true; 
                }
                Student UserToUpdate = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
                UserToUpdate.StudentLevel = NewLevel;
                _unitOfWork.StudentRepository.UpdateAsync(UserToUpdate);
                _unitOfWork.Save();

                await _message.SendAsync(text: $"Updated!\nName: {UserToUpdate.Name}\nLevel: {UserToUpdate.StudentLevel}");
                return true;
            }
            return false;
        }
        public async Task<bool> CheckUsersAccessToCallbackAsync()
        {
            if (user == null ^ !user.isRegistered)
            {
                await _message.SendAsync(text: "Please, register in this system in such way:" +
                "\n/reg {Name} {Surname} {Occupation (Student/Working)} {GitHub link}");

                return false;
            }
            return true;
        }
        public async Task<bool> CheckUsersRegistrationAsync()
        {
            if (user.isRegistered)
            {
                await _message.SendAsync(text: "You are already registered.");
                return false;
            }
            return true;
        }
        public async Task<InlineKeyboardMarkup> GenerateStudentsListKeyboardAsync(string CallBackTag)
        {
            IEnumerable<Student> Students = await _unitOfWork.StudentRepository.GetAllAsync();
            InlineKeyboardMarkup StudentsListInlineKeyboard = InlineKeyboards.GenerateStudentsListKeyboard(Students, CallBackTag);
            return StudentsListInlineKeyboard;
        }

        public async Task<bool> MentionStudentsAsync()
        {
            int MentionLevel;
            var Students = await _unitOfWork.StudentRepository.GetAllAsync();

            // Mention specific user level
            if (_update.CallbackQuery.Data.Contains("level_"))
            {
                MentionLevel = Int32.Parse(_update.CallbackQuery.Data.Split(" ")[2]);
                var students = Students.Where(s => s.StudentLevel == MentionLevel);
                List<String> TelegramTags = new List<string>();
                foreach (Student s in students)
                    TelegramTags.Add("@" + s.TelegramUserName);
                string Tags = string.Join(", ", TelegramTags);

                await _message.SendAsync($"Alert!\n" +
                        $"{Tags}");
                return true;
            }

            // Mention all
            else if (_update.CallbackQuery.Data.Contains("all_"))
            {
                // TODO: Transfer this to appinlinekeyboards

                List<String> TelegramTags = new List<string>();
                foreach (Student s in Students)
                    TelegramTags.Add("@" + s.TelegramUserName);
                string Tags = string.Join(", ", TelegramTags);

                await _message.SendAsync($"Alert!\n" +
                        $"{Tags}");
                return true;
            }
            return false;
        }

        public async void ReturnToStartKeyboardAsync()
        {
            await _message.EditMessageReplyMarkupAsync(InlineKeyboard: InlineKeyboardMenu);
        }

        public async Task<bool> PrintInfoAboutStudentAsync()
        {
            long UserTelegramId = Int64.Parse(_update.CallbackQuery.Data.Split(" ")[0]);
            Student UserAbout = await _unitOfWork.StudentRepository.GetByTelegramIdAsync(UserTelegramId);
            Subscription UserSubscription = await _unitOfWork.SubscriptionRepository.GetByStudentIdAsync(UserAbout.Id);
            string UserInfo = UserAbout.GetSubscriptionInfo(UserSubscription);

            await _message.SendAsync(text: UserInfo);
            return true;
        }

        public async Task<bool> CheckUserSubscriptionAsync()
        {
            if (user == null)
            {
                await _message.SendAsync(text: "You must register first!");
                return false;
            }

            Subscription UserSubscription = await _unitOfWork.SubscriptionRepository.GetByStudentIdAsync(user.Id);

            // If user is admin, print keyboard with all student to choose by click
            if (user.isAdmin)
            {
                await _message.SendWithReplyMarkupAsync(text: "Choose student to check",
                    InlineKeyboard: await GenerateStudentsListKeyboardAsync("student"));
                return true;
            }
            //If user is not admin, print info about him
            else
            {
                string Info = user.GetSubscriptionInfo(UserSubscription);
                await _message.SendAsync(text: Info);
                return true;
            }
        }

        public async void ManageAsync()
        {
            if (user == null)
            {
                await _message.SendAsync(text: "You must obtain admin rules!");
            }
            else if (user != null && !user.isAdmin)
            {
                await _message.SendAsync(text: "You must register first and obtain admin rules!");
            }

            // If user is registered and is admin
            else
            {
                await _message.EditMessageReplyMarkupAsync(InlineKeyboard: InlineKeyboardManageMenu);
            }
        }

        public async void HowToSetLevel()
        {
            await _message.SendAsync(text: "To set student's level, enter command:" +
                           "\n/setlevel {User's telegram id} {Student's level: 1/2}");
        }

        public async Task<bool> ListAllSudents()
        {
            var Students = await _unitOfWork.StudentRepository.GetAllAsync();
            List<string> OutputDataList = new List<string>();
            foreach (Student s in Students)
            {
                Subscription UserSubscription = await _unitOfWork.SubscriptionRepository.GetByStudentIdAsync(s.Id);
                string text = s.GetInfo(UserSubscription);
                OutputDataList.Add(text);
            }
            string OutputData = string.Join(" ", OutputDataList.ToArray());
            await _message.SendWithReplyMarkupAsync(OutputData, InlineKeyboardManageMenu);
            return true;
        }

        public async void HowToRenew()
        {
            string text = "To renew student's subscription, enter command:" +
                           "\nTo add an amount of months(from today if user never had a subscription or from the expiration date)" +
                           "\n/renew 0 {User's telegram id} {Amount of months to add}" +
                           "\nOR to set a specific date:" +
                           "\n/renew 1 {User's telegram id} {Date in format DD.MM.YYYY}";

            await _message.SendAsync(text);
        }

        public async Task<bool> GrantAdminAccess()
        {
            if (user != null && !user.isAdmin)
            {
                user.isAdmin = true;

                _unitOfWork.StudentRepository.UpdateAsync(user);
                _unitOfWork.Save();
                return true;
            }
            else
            {
                await _message.SendErrorAsync();
                return false;
            }
        }
    }
}
