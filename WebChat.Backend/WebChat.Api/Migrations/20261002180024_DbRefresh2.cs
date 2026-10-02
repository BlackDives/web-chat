using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebChat.Api.Migrations
{
    /// <inheritdoc />
    public partial class DbRefresh2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "date_of_birth",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "profile_picture_url",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "profile_picture_url",
                table: "users");
        }
    }
}
