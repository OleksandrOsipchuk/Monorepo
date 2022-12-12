using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace ITSadok.DotNetMentorship.Admin.Data.Entity
{
    public class Subscription
    {
        int Id { get; set; }
        bool isActive { get; set; }
        DateTime ExpirationDate { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
