using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroCadastro.Migrations
{
    public partial class projeto14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipodeRua",
                table: "Endereco");

            migrationBuilder.AlterColumn<double>(
                name: "CEP",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CEP",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "TipodeRua",
                table: "Endereco",
                nullable: true);
        }
    }
}
