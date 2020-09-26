using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "certificate_attachments",
                columns: table => new
                {
                    certificate_id = table.Column<int>(nullable: false),
                    attached_document_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_certificate_attachments", x => new { x.certificate_id, x.attached_document_id });
                    table.ForeignKey(
                        name: "fk_certificate_attachments_attached_documents_attached_documen",
                        column: x => x.attached_document_id,
                        principalTable: "attached_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_certificate_attachments_d_certificates_certificate_id",
                        column: x => x.certificate_id,
                        principalTable: "d_certificates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "c700fd73-07ba-40f4-ac20-1e292da24286");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "de281772-32fe-4f5a-aa42-a7ab89f2e4d5");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "a3b4fdd1-d307-401e-bd56-e5a9d8db3939");

            migrationBuilder.CreateIndex(
                name: "ix_certificate_attachments_attached_document_id",
                table: "certificate_attachments",
                column: "attached_document_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "certificate_attachments");

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
    }
}
