using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations
{
    public partial class ClientNomenclatureAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nom_clients",
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
                    is_active = table.Column<bool>(nullable: false),
                    adress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_nom_clients", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nom_clients");
        }
    }
}
