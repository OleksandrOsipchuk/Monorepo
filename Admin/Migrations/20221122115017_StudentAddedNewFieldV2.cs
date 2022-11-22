using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TgModerator.Migrations
{
    /// <inheritdoc />
    public partial class StudentAddedNewFieldV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Surname = table.Column<string>(type: "text", nullable: true),
                    Occupation = table.Column<string>(type: "text", nullable: true),
                    StudentLevel = table.Column<int>(type: "integer", nullable: true),
                    GithubLink = table.Column<string>(type: "text", nullable: true),
                    isAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: true),
                    TelegramUserName = table.Column<string>(type: "text", nullable: true),
                    SubscriptionUntil = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    isRegistered = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
