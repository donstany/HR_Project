using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class RemoveAttachmentTypeAsUnneceserySecondCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attached_documents_attachment_types_attachment_type_id",
                table: "attached_documents");

            migrationBuilder.DropIndex(
                name: "ix_attached_documents_attachment_type_id",
                table: "attached_documents");

            migrationBuilder.DropColumn(
                name: "attachment_type_id",
                table: "attached_documents");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "3eec61c4-f0f0-4541-8f50-740d99fffbb5");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "f4cdf71d-73c8-4a1f-a338-9260443eee61");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "34ce92fb-75d3-4dbf-a654-06bc7cb030d9");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "attachment_type_id",
                table: "attached_documents",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "f2922b6d-ad5c-4abd-99c2-1fa0d80607b4");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "d0eb4fef-f9c9-4988-9860-85d0d391a146");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "ca72f8a8-87d5-41a6-8331-240bd1ba52b0");

            migrationBuilder.CreateIndex(
                name: "ix_attached_documents_attachment_type_id",
                table: "attached_documents",
                column: "attachment_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_attached_documents_attachment_types_attachment_type_id",
                table: "attached_documents",
                column: "attachment_type_id",
                principalTable: "attachment_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
