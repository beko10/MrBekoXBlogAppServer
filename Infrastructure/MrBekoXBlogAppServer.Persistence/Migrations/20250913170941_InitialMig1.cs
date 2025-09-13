using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MrBekoXBlogAppServer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MapUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialMedias",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "UpdatedDate" },
                values: new object[,]
                {
                    { "01c7b4fc-2898-4a80-af7f-b018311efc35", "Yaşam", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "31e68485-dac5-4ac2-a76d-72c80838a109", "Yazılım", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { "43b62264-27ed-45bc-8714-8a887c1d108b", "Teknoloji", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "ContactInfos",
                columns: new[] { "Id", "Address", "CreatedDate", "Email", "MapUrl", "Phone", "UpdatedDate" },
                values: new object[] { "4ff33380-5726-4d98-ae0f-c3b3e0017928", "İstanbul, Türkiye", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "info@blogapp.com", "https://maps.google.com/example", "+90 555 555 55 55", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "CreatedDate", "Email", "IsRead", "Name", "Subject", "UpdatedDate" },
                values: new object[] { "1a9d8ffd-7d66-44ed-bfb7-232720a18230", "Sitenizi çok beğendim!", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "ahmet@example.com", false, "Ahmet Yılmaz", "Merhaba", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "SocialMedias",
                columns: new[] { "Id", "CreatedDate", "Icon", "Name", "UpdatedDate", "Url" },
                values: new object[,]
                {
                    { "09b72f9e-d1a2-40b4-802e-f81b3b2642ed", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "fa-github", "GitHub", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "https://github.com/username" },
                    { "740a685e-fe6e-4273-bf48-bfc517d5e8d2", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "fa-linkedin", "LinkedIn", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "https://linkedin.com/in/username" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "CategoryId", "Content", "CoverImageUrl", "CreatedDate", "PostImageUrl", "PublishedDate", "Title", "UpdatedDate" },
                values: new object[] { "30b81e45-a012-440d-bf6f-1a56a2e3d50f", "Berkay", "31e68485-dac5-4ac2-a76d-72c80838a109", "ASP.NET Core 9 ile Minimal API ve Onion Architecture kullanımı...", "/images/cover1.jpg", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "/images/post1.jpg", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Minimal API ile Onion Architecture", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "SocialMedias");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
