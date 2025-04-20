using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPontoMongoDb.Migrations
{
    /// <inheritdoc />
    public partial class TesteRelacionamentoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tab_banco_horas_tab_cadastro_user_id",
                table: "tab_banco_horas");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_perfil_tab_cadastro_user_id",
                table: "tab_perfil");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_registro_tab_cadastro_user_id",
                table: "tab_registro");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_solicitacao_tab_cadastro_user_id",
                table: "tab_solicitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_tab_solicitacao_tab_registro_id_registro",
                table: "tab_solicitacao");

            migrationBuilder.RenameColumn(
                name: "horara_extra",
                table: "tab_registro",
                newName: "hora_extra");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "hora_extra",
                table: "tab_registro",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "total_horas_trabalhadas",
                table: "tab_banco_horas",
                type: "time",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AddForeignKey(
                name: "FK_BancoHoras_Cadastro",
                table: "tab_banco_horas",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Perfil_Cadastro",
                table: "tab_perfil",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Registro_Cadastro",
                table: "tab_registro",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Cadastro",
                table: "tab_solicitacao",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacao_Registro",
                table: "tab_solicitacao",
                column: "id_registro",
                principalTable: "tab_registro",
                principalColumn: "id_registro",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BancoHoras_Cadastro",
                table: "tab_banco_horas");

            migrationBuilder.DropForeignKey(
                name: "FK_Perfil_Cadastro",
                table: "tab_perfil");

            migrationBuilder.DropForeignKey(
                name: "FK_Registro_Cadastro",
                table: "tab_registro");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Cadastro",
                table: "tab_solicitacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacao_Registro",
                table: "tab_solicitacao");

            migrationBuilder.RenameColumn(
                name: "hora_extra",
                table: "tab_registro",
                newName: "horara_extra");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "observacao",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "tab_solicitacao",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "horara_extra",
                table: "tab_registro",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "total_horas_trabalhadas",
                table: "tab_banco_horas",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0),
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_banco_horas_tab_cadastro_user_id",
                table: "tab_banco_horas",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_perfil_tab_cadastro_user_id",
                table: "tab_perfil",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_registro_tab_cadastro_user_id",
                table: "tab_registro",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_solicitacao_tab_cadastro_user_id",
                table: "tab_solicitacao",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tab_solicitacao_tab_registro_id_registro",
                table: "tab_solicitacao",
                column: "id_registro",
                principalTable: "tab_registro",
                principalColumn: "id_registro",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
