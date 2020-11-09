using Microsoft.EntityFrameworkCore.Migrations;

namespace Biblioteca_Api.Migrations
{
    public partial class RelacionPrestamoLibro_prestamoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
