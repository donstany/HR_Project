using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class CrewRenamedToTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "d_crews");

            migrationBuilder.CreateTable(
                name: "d_teams",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: true),
                    is_active = table.Column<bool>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    person_id = table.Column<int>(nullable: false),
                    project_role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_teams", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_teams_d_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "d_persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_teams_d_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "d_projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_teams_nom_project_role_project_role_id",
                        column: x => x.project_role_id,
                        principalTable: "nom_project_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "1",
                column: "concurrency_stamp",
                value: "29b2db82-73a5-4b9a-9fcf-e54696c7fe15");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "2",
                column: "concurrency_stamp",
                value: "262cecdd-a49a-4483-8ab7-e34227002f81");

            migrationBuilder.UpdateData(
                table: "identity_roles",
                keyColumn: "id",
                keyValue: "3",
                column: "concurrency_stamp",
                value: "42622bca-3502-4fae-bf4b-defc31ad1bd9");

            migrationBuilder.CreateIndex(
                name: "ix_d_teams_person_id",
                table: "d_teams",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_teams_project_role_id",
                table: "d_teams",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_teams_project_id_person_id_project_role_id",
                table: "d_teams",
                columns: new[] { "project_id", "person_id", "project_role_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "d_teams");

            migrationBuilder.CreateTable(
                name: "d_crews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    project_id = table.Column<int>(type: "integer", nullable: false),
                    project_role_id = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_crews", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_crews_d_persons_person_id",
                        column: x => x.person_id,
                        principalTable: "d_persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_crews_d_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "d_projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_crews_nom_project_role_project_role_id",
                        column: x => x.project_role_id,
                        principalTable: "nom_project_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_person_id",
                table: "d_crews",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_project_role_id",
                table: "d_crews",
                column: "project_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_project_id_person_id_project_role_id",
                table: "d_crews",
                columns: new[] { "project_id", "person_id", "project_role_id" },
                unique: true);
        }
    }
}
