using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TODO.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Lists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lists", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Completed = table.Column<sbyte>(type: "tinyint", nullable: false, defaultValue: (sbyte)0),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TodoListId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Lists_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 9, 47, 46, 798, DateTimeKind.Local).AddTicks(1479)),
                    ListId = table.Column<int>(type: "int", nullable: false),
                    ListId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Lists_ListId1",
                        column: x => x.ListId1,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, null, "Avengers: Infinity War", 1 },
                    { 2, null, "The Matrix", 2 },
                    { 3, null, "The Matrix", 3 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "ListId", "Name", "TodoListId", "UserId" },
                values: new object[,]
                {
                    { 1, null, 1, "Mystery", null, 1 },
                    { 2, null, 1, "Fantasy", null, 1 },
                    { 3, null, 1, "Drama", null, 1 },
                    { 4, null, 2, "Drama", null, 2 },
                    { 5, null, 2, "Action", null, 2 },
                    { 6, null, 2, "Horror", null, 2 },
                    { 7, null, 3, "Drama", null, 3 },
                    { 8, null, 3, "Action", null, 3 },
                    { 9, null, 3, "Thriller", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ListId", "ListId1", "PassWord", "UserName" },
                values: new object[,]
                {
                    { 3, 3, null, "AD3LK5Hd8EUOXqBmlbMhGyn5UEvvHOfiwjcbHIHzwmNn+h4UHvqUYFRhWMnuBsERZg==", "Spielberg3" },
                    { 1, 1, null, "ABTGns1rQTEUxCS7CsHY/78BHDyWfA/3Fh9qfW4kw/czW/WiWvE3WkSe+UCIDkC7Pw==", "Hitchcock1" },
                    { 2, 2, null, "AI07FcsYajp4dRh5eB2ZPylloXmFXPIQ95I7H4ZzwSfWS8dDGhzrvoTnJ6GG41X6sA==", "Nolan2" }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "Id",
                value: 3);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TodoListId",
                table: "Tasks",
                column: "TodoListId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ListId1",
                table: "Users",
                column: "ListId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Lists");
        }
    }
}
