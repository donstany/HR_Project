using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class DocumentTypeNomenclatureAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nom_document_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_number = table.Column<int>(nullable: false),
                    code = table.Column<string>(nullable: true),
                    label = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    date_start = table.Column<DateTime>(nullable: false),
                    date_end = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_document_types", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "67574efc-4f67-48e0-a7e2-1c35c6e45e87");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "2e109266-88f6-4c2a-bea0-9e843f9e1e18");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "9fb7c348-d77a-41d1-9ef7-8b5916d41bf7");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nom_document_types");

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
    }
}
