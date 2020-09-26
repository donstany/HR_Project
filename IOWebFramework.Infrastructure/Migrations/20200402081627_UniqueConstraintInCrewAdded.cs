using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class UniqueConstraintInCrewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_d_crews_project_id",
                table: "d_crews");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "b467664c-52af-4713-95ff-d295228b6c28");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "52f2f63e-15af-4390-b362-f18ab3685f25");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "99637684-6eb9-420f-8feb-71fb1affabff");

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_project_id_person_id_project_role_id",
                table: "d_crews",
                columns: new[] { "project_id", "person_id", "project_role_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_d_crews_project_id_person_id_project_role_id",
                table: "d_crews");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "10b9aabc-e17a-4428-8b37-f6ee8cbad032");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "85674b69-e2cf-45f8-adb1-075d0dbb4cc2");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "2da2f1c1-6515-4b42-97b4-b949313e5a54");

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_project_id",
                table: "d_crews",
                column: "project_id");
        }
    }
}
