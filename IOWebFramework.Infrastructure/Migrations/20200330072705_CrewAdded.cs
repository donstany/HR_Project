using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class CrewAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "d_crews",
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

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_person_id",
                table: "d_crews",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_project_id",
                table: "d_crews",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_crews_project_role_id",
                table: "d_crews",
                column: "project_role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "d_crews");
        }
    }
}
