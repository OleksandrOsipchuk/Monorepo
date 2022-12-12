using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace ITSadok.DotNetMentorship.Admin.Data.Entity
{
    public class TelegramUser
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string TelegramUsername { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
