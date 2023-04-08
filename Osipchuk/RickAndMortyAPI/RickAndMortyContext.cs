using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RickMorty;

namespace RickAndMortyAPI;

public class RickAndMortyContext : DbContext
{
    public virtual DbSet<Character> Characters { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseLowerCaseNamingConvention()
    .UseNpgsql("Host=localhost;Port=5432;Database=ramdb;Username=postgres;Password=2002");
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("characters_pkey");

            entity.ToTable("characters");
        });
    }
}
