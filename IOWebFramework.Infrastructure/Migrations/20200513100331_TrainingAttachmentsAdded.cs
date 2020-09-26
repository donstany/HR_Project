using Microsoft.EntityFrameworkCore.Migrations;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class TrainingAttachmentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "training_attachments",
                columns: table => new
                {
                    training_id = table.Column<int>(nullable: false),
                    attached_document_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_training_attachments", x => new { x.training_id, x.attached_document_id });
                    table.ForeignKey(
                        name: "fk_training_attachments_attached_documents_attached_document_id",
                        column: x => x.attached_document_id,
                        principalTable: "attached_documents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_training_attachments_d_trainings_training_id",
                        column: x => x.training_id,
                        principalTable: "d_trainings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "ix_training_attachments_attached_document_id",
                table: "training_attachments",
                column: "attached_document_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "training_attachments");

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
        }
    }
}
