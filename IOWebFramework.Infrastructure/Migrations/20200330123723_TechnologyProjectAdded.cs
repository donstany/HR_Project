using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class TechnologyProjectAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "d_technology_project",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(nullable: false),
                    technology_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_d_technology_project", x => x.id);
                    table.ForeignKey(
                        name: "fk_d_technology_project_d_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "d_projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_d_technology_project_nom_technologies_technology_id",
                        column: x => x.technology_id,
                        principalTable: "nom_technologies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_d_technology_project_project_id",
                table: "d_technology_project",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "ix_d_technology_project_technology_id",
                table: "d_technology_project",
                column: "technology_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "d_technology_project");
        }
    }
}
