using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MrBekoXBlogAppServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "u1234567-89ab-cdef-0123-456789abcdef");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldMaxLength: 300,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "FullName", "ImageUrl", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedDate", "UserName" },
                values: new object[] { "u1234567-89ab-cdef-0123-456789abcdef", 0, "SEED-CONCURRENCY-STAMP-123456789", new DateTime(2025, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "berkay@example.com", false, "Berkay Beko", "/images/users/berkay.jpg", false, null, "BERKAY@EXAMPLE.COM", "BERKAY", "AQAAAAIAAYagAAAAEHfk2/3QXmQzYFJ4vGm8N9p7UgLJJ2cR4Ke9Z5xP6oK3w8T1sY2nH7qV5bF9jL4mE3==", null, false, "SEED-SECURITY-STAMP-123456789", false, null, "berkay" });
        }
    }
}
