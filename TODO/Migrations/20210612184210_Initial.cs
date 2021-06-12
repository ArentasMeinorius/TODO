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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2021, 6, 12, 21, 42, 9, 911, DateTimeKind.Local).AddTicks(6438)),
                    ListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                    table.ForeignKey(
                        name: "FK_Lists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Lists_ListId",
                        column: x => x.ListId,
                        principalTable: "Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ListId", "PassWord", "UserName" },
                values: new object[] { 3, 3, "AIj5w8axa7cgIDAXFYkKFeP9c1D8IJhhaFXI+70BDia8UUkdDVYHzkQ6no/B1gqfhA==", "Spielberg" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ListId", "PassWord", "UserName" },
                values: new object[] { 1, 1, "AN6NMQ6YNVrPYuFjKlrK2/2q8P+THIL4qxW9e1f1i7DWoMWpeXZPFrtfWnVJauBV3Q==", "Scorsese" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ListId", "PassWord", "UserName" },
                values: new object[] { 2, 2, "AH/AEHfhG5mlXI7jd5UT9MPzLUqnKgIZFZT+3qoiEwjICuZKpiUr/ZWZ5ecnUevrtg==", "Spielberg" });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "Id",
                value: 3);

            migrationBuilder.InsertData(
                table: "Lists",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { 3, null, "Joker", 3 },
                    { 1, null, "Joker", 1 },
                    { 2, null, "Avengers: Infinity War", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "ListId", "Name" },
                values: new object[,]
                {
                    { 7, null, 3, "Western" },
                    { 8, null, 3, "Action" },
                    { 9, null, 3, "Horror" },
                    { 1, null, 1, "Drama" },
                    { 2, null, 1, "Mystery" },
                    { 3, null, 1, "Fantasy" },
                    { 4, null, 2, "Thriller" },
                    { 5, null, 2, "Romance" },
                    { 6, null, 2, "Romance" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lists_UserId",
                table: "Lists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ListId",
                table: "Tasks",
                column: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Lists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
