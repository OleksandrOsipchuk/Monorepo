using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Data.Entity
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
        public bool isRegistered { get; set; }
        public Subscription? Subscription { get; set; }



        public string GetInfo()
        {
            string text = $"\nId: {Id}\nName: {Name} {Surname} - {Occupation} - Level: {StudentLevel}" +
                $"\nGithub Link: {GithubLink} \nTelegram Username: @{TelegramUserName}" +
                $"\nTelegram Id: {TelegramId}";

            if (Subscription != null && !(bool)Subscription.IsExpired)
            {
                string SubscriptionExpirationDate = Subscription.ExpirationDate.ToString();
                text += $"\nSubscription status: Active" +
                $"\nExpiration Date: {SubscriptionExpirationDate}";

            }
            else
            {
                text += $"\nSubscription status: Expired" +
                    $"\n----------";
            }
            return text;
        }

        public string GetSubscriptionInfo()
        {
            string text = $"\nName: {Name} {Surname} - {Occupation} - Level: {StudentLevel}";
            
            if (Subscription != null && !(bool)Subscription.IsExpired)
            {
                string SubscriptionExpirationDate = Subscription.ExpirationDate.ToString();
                text += $"\nSubscription status: Active" +
                $"\nExpiration Date: {SubscriptionExpirationDate}";
            }
            else if (Subscription != null && (bool)Subscription.IsExpired)
            {
                text += $"\nSubscription status: Expired_Null";
            }
            else
            {
                text += $"\nSubscription status: Expired";
            }
                
            return text;
        }
    }
}
