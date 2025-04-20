using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPontoMongoDb.Migrations
{
    /// <inheritdoc />
    public partial class AddTabelaRegistroAtual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "horario",
                table: "tab_registro",
                newName: "horara_extra");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "total_hora",
                table: "tab_registro",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "total_hora",
                table: "tab_registro");

            migrationBuilder.RenameColumn(
                name: "horara_extra",
                table: "tab_registro",
                newName: "horario");
        }
    }
}
