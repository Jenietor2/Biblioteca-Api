using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca_Api.Migrations
{
    public partial class alterusuarioId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Usuarios_UsuarioID",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "UsuarioID",
                table: "Libros",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Libros_UsuarioID",
                table: "Libros",
                newName: "IX_Libros_UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Usuarios_UsuarioId",
                table: "Libros",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Usuarios_UsuarioId",
                table: "Libros");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Libros",
                newName: "UsuarioID");

            migrationBuilder.RenameIndex(
                name: "IX_Libros_UsuarioId",
                table: "Libros",
                newName: "IX_Libros_UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Usuarios_UsuarioID",
                table: "Libros",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
