using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;

namespace Admin.Data.Repository
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

