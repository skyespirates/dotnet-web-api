using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace project_service.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    student_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.student_id);
                });

            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    book_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    borrower_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.book_id);
                    table.ForeignKey(
                        name: "FK_Books_Students_borrower_id",
                        column: x => x.borrower_id,
                        principalTable: "Students",
                        principalColumn: "student_id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Books_borrower_id",
                table: "Books",
                column: "borrower_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Todos");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
