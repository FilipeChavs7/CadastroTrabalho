using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroCadastro.Migrations
{
    public partial class versão12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Pessoa_PessoaId",
                table: "Endereco");

            migrationBuilder.AlterColumn<int>(
                name: "PessoaId",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
