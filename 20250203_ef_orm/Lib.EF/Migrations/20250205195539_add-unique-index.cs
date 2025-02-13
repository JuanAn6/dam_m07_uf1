using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lib.EF.Migrations
{
    public partial class adduniqueindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Departaments",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.CreateIndex(
                name: "IX_Departaments_Nom",
                table: "Departaments",
                column: "Nom",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Departaments_Nom",
                table: "Departaments");

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "Departaments",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200);
        }
    }
}
