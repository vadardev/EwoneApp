using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ewone.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WordId = table.Column<int>(type: "integer", nullable: false),
                    Definition = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Examples = table.Column<List<string>>(type: "text[]", nullable: false),
                    ParentId = table.Column<int>(type: "integer", nullable: true),
                    AuthorId = table.Column<int>(type: "integer", nullable: true),
                    ModuleId = table.Column<int>(type: "integer", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_Card_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Card",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Card_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "CreateDate", "Description", "Name", "UserId" },
                values: new object[] { 1, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), null, "Default", null });

            migrationBuilder.InsertData(
                table: "Words",
                columns: new[] { "Id", "CreateDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), "book" },
                    { 2, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), "apple" },
                    { 3, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), "cat" }
                });

            migrationBuilder.InsertData(
                table: "Card",
                columns: new[] { "Id", "AuthorId", "CreateDate", "Definition", "Examples", "ImageUrl", "ModuleId", "ParentId", "WordId" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), "a written text that can be published in printed or electronic form", new List<string> { "We are reading a different book this week" }, "https://dictionary.cambridge.org/images/thumb/book_noun_001_01679.jpg", 1, null, 1 },
                    { 2, null, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), "a round fruit with firm, white flesh and a green, red, or yellow skin", new List<string> { "The apple tree at the bottom of the garden is beginning to blossom" }, "https://dictionary.cambridge.org/images/thumb/apple_noun_001_00650.jpg", 1, null, 2 },
                    { 3, null, new DateTime(2023, 2, 13, 15, 37, 5, 741, DateTimeKind.Utc).AddTicks(1570), "a small animal with fur, four legs, a tail, and claws, usually kept as a pet or for catching mice", new List<string> { "My cat likes dozing in front of the fire" }, "https://dictionary.cambridge.org/images/thumb/cat_noun_001_02368.jpg", 1, null, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_AuthorId",
                table: "Card",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_ModuleId",
                table: "Card",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_ParentId",
                table: "Card",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_WordId",
                table: "Card",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_UserId",
                table: "Modules",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
