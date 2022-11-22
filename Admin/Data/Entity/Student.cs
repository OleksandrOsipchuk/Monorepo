using System.ComponentModel.DataAnnotations.Schema;

namespace TgModerator.Data.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Occupation { get; set; }
        public int? StudentLevel { get; set; }
        public string? GithubLink { get; set; }
        public bool isAdmin { get; set; }
        public long? TelegramId { get; set; }
        public string? TelegramUserName { get; set; }
        public DateTime SubscriptionUntil { get; set; }
        public bool isRegistered { get; set; }

        public string GetInfo()
        {
            string SubscriptionStatus;
            if (this.SubscriptionUntil > DateTime.Now)
            {
                SubscriptionStatus = "Active";
            }
            else
            {
                SubscriptionStatus = "Expired";
            }
            string text = $"\nId: {this.Id}\nName: {this.Name} {this.Surname} - {this.Occupation} - Level: {this.StudentLevel}" +
                $"\nGithub Link: {this.GithubLink} \nTelegram Username: @{this.TelegramUserName}" +
                $"\nTelegram Id: {this.TelegramId}" +
                $"\nSubscription status: {SubscriptionStatus}" +
                $"\nExpiration Date: {this.SubscriptionUntil}" +
                $"\n----------";
            return text;
        }

        public string GetSubscriptionInfo()
        {
            string SubscriptionStatus;
            if (this.SubscriptionUntil > DateTime.Now)
            {
                SubscriptionStatus = "Active";
            }
            else
            {
                SubscriptionStatus = "Expired";
            }
            string text = $"\nName: {this.Name} {this.Surname} - {this.Occupation} - Level: {this.StudentLevel}" +
                $"\nSubscription status: {SubscriptionStatus}" +
                $"\nExpiration Date: {this.SubscriptionUntil}";
            return text;
        }
    }
}
