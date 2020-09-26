using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class RemovedAttachmentTypeFromDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "attachment_types");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "2e5bd116-0f65-4cfb-80fc-98c2124da895");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "b077da40-29ca-4e08-9324-05e169327814");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "fea4b8c9-641f-4e6e-af16-538dea2c8ade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attachment_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: true),
                    date_end = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    date_start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    label = table.Column<string>(type: "text", nullable: false),
                    order_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attachment_types", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "1ab901d8-1423-4918-92fb-946f08bfbe93");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "020f00e3-5dd7-4eef-ba24-b2fa5e341af9");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "395d663a-7c29-407b-8402-3129b1df8040");
        }
    }
}
