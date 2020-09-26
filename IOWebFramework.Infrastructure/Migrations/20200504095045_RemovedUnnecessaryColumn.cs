using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class RemovedUnnecessaryColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity_id",
                table: "attached_documents");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "f67793e2-f7bd-455c-a4b7-a94c650ca0df");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "badc6935-fed0-44d8-8274-a69f266da702");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "d5de4281-8b82-4fe1-8254-b23f29c05bc1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "activity_id",
                table: "attached_documents",
                type: "integer",
                nullable: true);

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
        }
    }
}
