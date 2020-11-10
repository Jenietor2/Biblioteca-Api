using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca_Api.Migrations
{
    public partial class RelacionUsuarioLibro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Libros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_UsuarioID",
                table: "Libros",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Usuarios_UsuarioID",
                table: "Libros",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Usuarios_UsuarioID",
                table: "Libros");

            migrationBuilder.DropIndex(
                name: "IX_Libros_UsuarioID",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Libros");
        }
    }
}
