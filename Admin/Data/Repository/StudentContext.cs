using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using TgModerator.Data.Entity;

namespace TgModerator.Data.Repository
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here

            //Property Configurations
            modelBuilder.Entity<Student>()
                    .Property(u => u.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedOnAdd()
                    .IsRequired();
            modelBuilder.Entity<Student>()
                    .Property(u => u.SubscriptionUntil)
                    .HasDefaultValue(DateTime.MinValue);

    }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

