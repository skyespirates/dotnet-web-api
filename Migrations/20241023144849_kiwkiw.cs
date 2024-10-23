using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace project_service.Migrations
{
    /// <inheritdoc />
    public partial class kiwkiw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "book_id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "student_id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "student_id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "student_id",
                keyValue: 6);

            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { 1, "skyes", "test123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "Students");

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "student_id", "student_name" },
                values: new object[,]
                {
                    { 5, "Zilong" },
                    { 6, "Freya" },
                    { 7, "Martis" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "book_id", "author", "book_title", "borrower_id" },
                values: new object[,]
                {
                    { 10, "Eichirou Oda", "One Piece", 5 },
                    { 11, "Hajime Isayama", "Shingeki No Kyoujin", 5 },
                    { 12, "Yusuke Murate", "One Punch Man", 6 }
                });
        }
    }
}
