using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class UniqueConstraintInTechnologyProjectAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_d_technology_project_project_id",
                table: "d_technology_project");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "2f3ac804-8c0e-45c8-a853-4f3e6fa9b9be");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "c98c5abd-a836-427a-a215-99596924f523");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "ba86437a-0941-4c16-aa44-038086caa002");

            migrationBuilder.CreateIndex(
                name: "ix_d_technology_project_project_id_technology_id",
                table: "d_technology_project",
                columns: new[] { "project_id", "technology_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_d_technology_project_project_id_technology_id",
                table: "d_technology_project");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "7e5a1cd4-7106-438a-9200-6f30a7831a84");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "0135bac7-066f-4874-b55d-cadd3a3fda5c");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "668c5ea8-457a-42cd-a848-0db3d0e2b704");

            migrationBuilder.CreateIndex(
                name: "ix_d_technology_project_project_id",
                table: "d_technology_project",
                column: "project_id");
        }
    }
}
