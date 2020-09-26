using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IOWebFramework.Infrastructure.Migrations.CdnDb
{
    public partial class CdnDbContextAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cdn_files",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    source_type = table.Column<int>(nullable: false),
                    source_id = table.Column<string>(nullable: false),
                    content_id = table.Column<string>(maxLength: 50, nullable: true),
                    file_name = table.Column<string>(maxLength: 500, nullable: false),
                    file_title = table.Column<string>(nullable: true),
                    file_description = table.Column<string>(nullable: true),
                    file_size = table.Column<int>(nullable: false),
                    date_uploaded = table.Column<DateTime>(nullable: false),
                    user_uploaded = table.Column<string>(maxLength: 500, nullable: true),
                    tenant_id = table.Column<string>(maxLength: 50, nullable: true),
                    is_active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdn_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cdn_file_contents",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cdn_file_id = table.Column<long>(nullable: false),
                    content = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cdn_file_contents", x => x.id);
                    table.ForeignKey(
                        name: "FK_cdn_file_contents_cdn_files_cdn_file_id",
                        column: x => x.cdn_file_id,
                        principalTable: "cdn_files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cdn_file_contents_cdn_file_id",
                table: "cdn_file_contents",
                column: "cdn_file_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cdn_file_contents");

            migrationBuilder.DropTable(
                name: "cdn_files");
        }
    }
}
