using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroCadastro.Migrations
{
    public partial class FinalTest10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pessoa_PessoaId",
                table: "Endereco");

            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "Pessoa",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoDeRua",
                table: "Endereco",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pessoa_PessoaId",
                table: "Endereco",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pessoa_PessoaId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "TipoDeRua",
                table: "Endereco");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Pessoa_PessoaId",
                table: "Endereco",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
