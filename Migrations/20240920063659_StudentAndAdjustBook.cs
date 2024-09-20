using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_service.Migrations
{
    /// <inheritdoc />
    public partial class StudentAndAdjustBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "borrower_id",
                table: "Books",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Books_borrower_id",
                table: "Books",
                column: "borrower_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Students_borrower_id",
                table: "Books",
                column: "borrower_id",
                principalTable: "Students",
                principalColumn: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Students_borrower_id",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Books_borrower_id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "borrower_id",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "author",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
