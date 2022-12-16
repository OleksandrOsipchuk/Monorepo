using ITSadok.DotNetMentorship.Admin.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ITSadok.DotNetMentorship.Admin.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
         public AppDbContext() : base()
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
            
            modelBuilder.Entity<Subscription>()
                .HasKey(su => su.Id);

            modelBuilder.Entity<TelegramUser>()
                .HasKey(tu => tu.Id);

            modelBuilder.Entity<Student>()
                .HasOne(tu => tu.TelegramUser)
                .WithOne(tu => tu.Student)
                .HasForeignKey<TelegramUser>(tu => tu.StudentId);

            modelBuilder.Entity<Student>()
                .HasOne(su => su.Subscription)
                .WithOne(s => s.Student)
                .HasForeignKey<Subscription>(su => su.StudentId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
}
