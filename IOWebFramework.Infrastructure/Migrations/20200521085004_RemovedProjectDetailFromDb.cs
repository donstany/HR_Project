using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class RemovedProjectDetailFromDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "d_project_details");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "e7da7333-127a-4b80-87d9-ec83ba727323");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "3823a56f-681d-449b-88d2-9fc4944e33ce");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "27ac8065-3034-491e-b057-b1c681319e84");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "d_project_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_id = table.Column<int>(type: "integer", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    project_role_id = table.Column<int>(type: "integer", nullable: false),
                    technology_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_project_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_project_details_d_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "d_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_project_details_d_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "d_projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_project_details_nom_project_role_project_role_id",
                        column: x => x.project_role_id,
                        principalTable: "nom_project_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_project_details_nom_technologies_technology_id",
                        column: x => x.technology_id,
                        principalTable: "nom_technologies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_employee_id",
                table: "d_project_details",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_project_id",
                table: "d_project_details",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_project_role_id",
                table: "d_project_details",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_project_details_technology_id",
                table: "d_project_details",
                column: "technology_id");
        }
    }
}
