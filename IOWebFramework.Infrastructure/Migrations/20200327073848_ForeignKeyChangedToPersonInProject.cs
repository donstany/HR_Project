using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class ForeignKeyChangedToPersonInProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_d_projects_d_employees_manager_id",
                table: "d_projects");

            migrationBuilder.AddForeignKey(
                name: "fk_d_projects_d_persons_manager_id",
                table: "d_projects",
                column: "manager_id",
                principalTable: "d_persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_d_projects_d_persons_manager_id",
                table: "d_projects");

            migrationBuilder.AddForeignKey(
                name: "fk_d_projects_d_employees_manager_id",
                table: "d_projects",
                column: "manager_id",
                principalTable: "d_employees",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
