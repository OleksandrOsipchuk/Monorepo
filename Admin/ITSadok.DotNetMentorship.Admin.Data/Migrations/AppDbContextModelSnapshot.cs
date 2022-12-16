﻿// <auto-generated />
using ITSadok.DotNetMentorship.Admin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ITSadok.DotNetMentorship.Admin.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ITSadok.DotNetMentorship.Admin.Data.Entity.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("GithubLink")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRegistered")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StudentLevel")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ITSadok.DotNetMentorship.Admin.Data.Entity.Subscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("ITSadok.DotNetMentorship.Admin.Data.Entity.TelegramUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<long>("TelegramId")
                        .HasColumnType("bigint");

                    b.Property<string>("TelegramUsername")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("TelegramUsers");
                });

            modelBuilder.Entity("ITSadok.DotNetMentorship.Admin.Data.Entity.Subscription", b =>
                {
                    b.HasOne("ITSadok.DotNetMentorship.Admin.Data.Entity.Student", "Student")
                        .WithOne("Subscription")
                        .HasForeignKey("ITSadok.DotNetMentorship.Admin.Data.Entity.Subscription", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ITSadok.DotNetMentorship.Admin.Data.Entity.TelegramUser", b =>
                {
                    b.HasOne("ITSadok.DotNetMentorship.Admin.Data.Entity.Student", "Student")
                        .WithOne("TelegramUser")
                        .HasForeignKey("ITSadok.DotNetMentorship.Admin.Data.Entity.TelegramUser", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("ITSadok.DotNetMentorship.Admin.Data.Entity.Student", b =>
                {
                    b.Navigation("Subscription");

                    b.Navigation("TelegramUser");
                });
#pragma warning restore 612, 618
        }
    }
}
