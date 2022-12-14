using System;
using System.ComponentModel.DataAnnotations;

namespace ITSadok.DotNetMentorship.Admin.Data.Entity
{
    public class Subscription
    {
        [Key]
        int Id { get; set; }
        public bool IsActive { get; set; }
        DateTime ExpirationDate { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
