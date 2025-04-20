using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPontoMongoDb.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaRegistro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data",
                table: "tab_registro",
                newName: "data_inicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "fim",
                table: "tab_registro",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "saida_almoco",
                table: "tab_registro",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "volta_almoco",
                table: "tab_registro",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fim",
                table: "tab_registro");

            migrationBuilder.DropColumn(
                name: "saida_almoco",
                table: "tab_registro");

            migrationBuilder.DropColumn(
                name: "volta_almoco",
                table: "tab_registro");

            migrationBuilder.RenameColumn(
                name: "data_inicio",
                table: "tab_registro",
                newName: "data");
        }
    }
}
