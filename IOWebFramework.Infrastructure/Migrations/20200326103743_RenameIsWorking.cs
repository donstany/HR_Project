using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class RenameIsWorking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_leaved",
                table: "d_employees");

            migrationBuilder.AddColumn<bool>(
                name: "is_working",
                table: "d_employees",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_working",
                table: "d_employees");

            migrationBuilder.AddColumn<bool>(
                name: "is_leaved",
                table: "d_employees",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
