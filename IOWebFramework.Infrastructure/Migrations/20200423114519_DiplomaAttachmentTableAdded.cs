using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class DiplomaAttachmentTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "attachment_types",
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
                    table.PrimaryKey("pk_attachment_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "attached_documents",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    attachment_type_id = table.Column<int>(nullable: false),
                    activity_id = table.Column<int>(nullable: true),
                    number = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: true),
                    has_file = table.Column<bool>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    file_content_id = table.Column<string>(nullable: true),
                    date_uploaded = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_attached_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_attached_documents_attachment_types_attachment_type_id",
                        column: x => x.attachment_type_id,
                        principalTable: "attachment_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "diploma_attachments",
                columns: table => new
                {
                    diploma_id = table.Column<int>(nullable: false),
                    attached_document_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_diploma_attachments", x => new { x.diploma_id, x.attached_document_id });
                    table.ForeignKey(
                        name: "fk_diploma_attachments_attached_documents_attached_document_id",
                        column: x => x.attached_document_id,
                        principalTable: "attached_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_diploma_attachments_d_diplomas_diploma_id",
                        column: x => x.diploma_id,
                        principalTable: "d_diplomas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ix_attached_documents_attachment_type_id",
                table: "attached_documents",
                column: "attachment_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_diploma_attachments_attached_document_id",
                table: "diploma_attachments",
                column: "attached_document_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diploma_attachments");

            migrationBuilder.DropTable(
                name: "attached_documents");

            migrationBuilder.DropTable(
                name: "attachment_types");

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
        }
    }
}
