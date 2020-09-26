using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class DefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "identity_roles",
                columns: new[] { "id", "concurrency_stamp", "name", "normalized_name" },
                values: new object[,]
                {
                    { "1", "10b9aabc-e17a-4428-8b37-f6ee8cbad032", "HR", "HR" },
                    { "2", "85674b69-e2cf-45f8-adb1-075d0dbb4cc2", "Employee", "EMPLOYEE" },
                    { "3", "2da2f1c1-6515-4b42-97b4-b949313e5a54", "SuperAdmin", "SUPERADMIN" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3");
        }
    }
}
