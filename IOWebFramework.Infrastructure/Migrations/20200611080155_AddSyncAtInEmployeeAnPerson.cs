using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class AddSyncAtInEmployeeAnPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "synced_at",
                table: "d_persons",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 1, 0, 0, 0, 500, DateTimeKind.Local).AddTicks(1000));

            migrationBuilder.AddColumn<DateTime>(
                name: "synced_at",
                table: "d_employees",
                nullable: false,
                defaultValue: new DateTime(2020, 1, 1, 0, 0, 0, 500, DateTimeKind.Local).AddTicks(1000));

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "ca1bc638-1652-42fc-8d08-6c71a24196ec");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "3755f093-6ade-4f5c-924b-3960bf18b6cd");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "f727a74c-9f9b-4979-8973-80b6cdeeacc8");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "synced_at",
                table: "d_persons");

            migrationBuilder.DropColumn(
                name: "synced_at",
                table: "d_employees");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "f68a0ad9-daa7-4520-ac0f-6eedb13b2f97");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "3d7e3e5c-0506-430e-8348-1f9203e183b8");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "61066dc5-8482-461c-ab6c-a11f45852c9a");
        }
    }
}
