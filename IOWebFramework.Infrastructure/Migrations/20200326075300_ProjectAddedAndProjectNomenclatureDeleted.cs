using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class ProjectAddedAndProjectNomenclatureDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_d_project_details_nom_projects_project_id",
                table: "d_project_details");

            migrationBuilder.DropTable(
                name: "nom_projects");

            migrationBuilder.CreateTable(
                name: "d_projects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    full_name = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    client_id = table.Column<int>(nullable: false),
                    manager_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_projects", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_projects_nom_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "nom_clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_projects_d_employees_manager_id",
                        column: x => x.manager_id,
                        principalTable: "d_employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_d_projects_client_id",
                table: "d_projects",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_projects_manager_id",
                table: "d_projects",
                column: "manager_id");

            migrationBuilder.AddForeignKey(
                name: "fk_d_project_details_d_projects_project_id",
                table: "d_project_details",
                column: "project_id",
                principalTable: "d_projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_d_project_details_d_projects_project_id",
                table: "d_project_details");

            migrationBuilder.DropTable(
                name: "d_projects");

            migrationBuilder.CreateTable(
                name: "nom_projects",
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
                    table.PrimaryKey("pk_nom_projects", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_d_project_details_nom_projects_project_id",
                table: "d_project_details",
                column: "project_id",
                principalTable: "nom_projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
