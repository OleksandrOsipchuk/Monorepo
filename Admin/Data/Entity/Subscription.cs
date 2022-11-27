namespace Admin.Data.Entity

{
    public class Subscription
    {
        public int Id { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int StudentForeignKey { get; set; }
        public Student? Student { get; set; }
    }
}
