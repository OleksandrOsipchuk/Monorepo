using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RickMorty;

namespace RickAndMortyAPI;

public partial class RamdbContext : DbContext
{
    public virtual DbSet<Character> Characters { get; set; }
    public RamdbContext(DbContextOptions<RamdbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("characters_pkey");

            entity.ToTable("characters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Species).HasColumnName("species");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Url).HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
