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
        public DateTime? SubscriptionUntil { get; set; }
        public bool isRegistered { get; set; }
    }
}
