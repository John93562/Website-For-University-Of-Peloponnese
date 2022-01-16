using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeluzionalPenguinz.DataAccess.Migrations
{
    public partial class addedHumanType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HumanType",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HumanType",
                table: "AspNetUsers");
        }
    }
}
