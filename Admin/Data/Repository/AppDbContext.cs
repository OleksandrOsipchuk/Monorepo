using System.Collections.Generic;
using System.Reflection.Metadata;
using Admin.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace Admin.Data.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Property Configurations
            modelBuilder.Entity<Student>()
                    .Property(s => s.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd()
                    .IsRequired();
            
            modelBuilder.Entity<Student>()
                    .HasOne(s => s.Subscription).WithOne(subs => subs.Student)
                    .HasForeignKey<Subscription>(subs => subs.StudentForeignKey);

            modelBuilder.Entity<Subscription>()
                    .Property(s => s.IsExpired)
                    .HasDefaultValue(true);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

