using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca_Api.Migrations
{
    public partial class addFieldLibroRuta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ruta",
                table: "Libros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ruta",
                table: "Libros");
        }
    }
}
