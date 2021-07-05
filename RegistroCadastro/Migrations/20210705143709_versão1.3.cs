using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroCadastro.Migrations
{
    public partial class versão13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Pessoa");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Pessoa",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Endereco",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Endereco",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Pessoa",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Pessoa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Logradouro",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Endereco",
                maxLength: 2,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Complemento",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Cidade",
                table: "Endereco",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
