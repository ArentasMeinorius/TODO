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
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 13, 11, 4, 40, 547, DateTimeKind.Local).AddTicks(8222)),
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
                    { 1, null, "12 Angry Men", 1 },
                    { 2, null, "Avengers: Infinity War", 2 },
                    { 3, null, "Forrest Gump", 3 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "ListId", "Name", "TodoListId", "UserId" },
                values: new object[,]
                {
                    { 1, null, 1, "Romance", null, 1 },
                    { 2, null, 1, "Western", null, 1 },
                    { 3, null, 1, "Drama", null, 1 },
                    { 4, null, 2, "Romance", null, 2 },
                    { 5, null, 2, "Horror", null, 2 },
                    { 6, null, 2, "Western", null, 2 },
                    { 7, null, 3, "Drama", null, 3 },
                    { 8, null, 3, "Comedy", null, 3 },
                    { 9, null, 3, "Western", null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ListId", "ListId1", "PassWord", "UserName" },
                values: new object[,]
                {
                    { 3, 3, null, "ALqCSSNEXiR9NIZyYKZfb7RxGETmHN7AF7oWi7DRQt3vEBHc7JhmyrzYxRPTcrqYyQ==", "Spielberg@gmail.com3" },
                    { 1, 1, null, "ALk0SFF27J/KzzOwNNQVio4sioq4H+hijLZ/rtBO6U/pN+Bi41l8+nDDinMhB0AxNg==", "Tarantino@gmail.com1" },
                    { 2, 2, null, "AGBTbw6rB+WS6gBbViD1PKSHHbnUrt4SEDjkl6gS4PtFddyI9j2mF6aCGuDZiMajIg==", "Kubrick@gmail.com2" }
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
