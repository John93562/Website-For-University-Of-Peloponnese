using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeluzionalPenguinz.DataAccess.Migrations
{
    public partial class conflicts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfessorId",
                table: "Anouncements",
                newName: "ProfessorSomething");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfessorSomething",
                table: "Anouncements",
                newName: "ProfessorId");
        }
    }
}
