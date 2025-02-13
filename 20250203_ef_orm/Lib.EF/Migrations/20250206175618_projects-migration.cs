using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lib.EF.Migrations
{
    public partial class projectsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projectes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projectes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmpleatProjecte",
                columns: table => new
                {
                    EmpleatsId = table.Column<int>(type: "int", nullable: false),
                    ProjectesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleatProjecte", x => new { x.EmpleatsId, x.ProjectesId });
                    table.ForeignKey(
                        name: "FK_EmpleatProjecte_Empleats_EmpleatsId",
                        column: x => x.EmpleatsId,
                        principalTable: "Empleats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpleatProjecte_Projectes_ProjectesId",
                        column: x => x.ProjectesId,
                        principalTable: "Projectes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_EmpleatProjecte_ProjectesId",
                table: "EmpleatProjecte",
                column: "ProjectesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmpleatProjecte");

            migrationBuilder.DropTable(
                name: "Projectes");
        }
    }
}
