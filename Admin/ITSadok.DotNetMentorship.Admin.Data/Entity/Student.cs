using System;
using System.Collections.Generic;
using System.Text;

namespace ITSadok.DotNetMentorship.Admin.Data.Entity
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public enum StudentLevel
        {
            EntryLevel = 1,
            AdvancedLevel
        }
        public string GithubLink { get; set; }
        bool isAdmin { get; set; }
        bool isRegistered { get; set; }
        public TelegramUser TelegramUser { get; set; }
        public Subscription Subscription { get; set; }
    }
}
