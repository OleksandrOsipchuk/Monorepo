using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITSadok.DotNetMentorship.Admin.Data.Entity
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public StudentLevel StudentLevel { get; set;}
        public string? GithubLink { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsRegistered { get; set; }
        public TelegramUser? TelegramUser { get; set; }
        public Subscription? Subscription { get; set; }
    }
}
