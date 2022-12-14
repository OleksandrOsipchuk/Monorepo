using ITSadok.DotNetMentorship.Admin.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace ITSadok.DotNetMentorship.Admin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<TelegramUser> TelegramUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                    .Property(s => s.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();

            modelBuilder.Entity<Student>()
                .HasOne(u => u.TelegramUser)
                .WithOne(tu => tu.Student)
                .HasForeignKey<TelegramUser>(tu => tu.StudentId);

            modelBuilder.Entity<Student>()
                .HasOne(u => u.Subscription)
                .WithOne(s => s.Student)
                .HasForeignKey<Subscription>(s => s.StudentId);
        }
    }
}
