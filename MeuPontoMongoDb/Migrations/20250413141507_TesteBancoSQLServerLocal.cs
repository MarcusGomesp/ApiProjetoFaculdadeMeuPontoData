using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPontoMongoDb.Migrations
{
    /// <inheritdoc />
    public partial class TesteBancoSQLServerLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tab_cadastro",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    departamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cpf = table.Column<long>(type: "bigint", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_cadastro", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tab_banco_horas",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_horas_trabalhadas = table.Column<TimeSpan>(type: "time", nullable: false),
                    qtd_solicitacoes_pendentes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_banco_horas", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_tab_banco_horas_tab_cadastro_user_id",
                        column: x => x.user_id,
                        principalTable: "tab_cadastro",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tab_perfil",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UrlProfilePic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_perfil", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_tab_perfil_tab_cadastro_UserId",
                        column: x => x.UserId,
                        principalTable: "tab_cadastro",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tab_registro",
                columns: table => new
                {
                    id_registro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    qtde_batidas = table.Column<int>(type: "int", nullable: false),
                    horario = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_registro", x => x.id_registro);
                    table.ForeignKey(
                        name: "FK_tab_registro_tab_cadastro_user_id",
                        column: x => x.user_id,
                        principalTable: "tab_cadastro",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tab_solicitacao",
                columns: table => new
                {
                    id_solicitacao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    id_registro = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    horario = table.Column<TimeSpan>(type: "time", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    observacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tab_solicitacao", x => x.id_solicitacao);
                    table.ForeignKey(
                        name: "FK_tab_solicitacao_tab_cadastro_user_id",
                        column: x => x.user_id,
                        principalTable: "tab_cadastro",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tab_solicitacao_tab_registro_id_registro",
                        column: x => x.id_registro,
                        principalTable: "tab_registro",
                        principalColumn: "id_registro",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tab_registro_user_id",
                table: "tab_registro",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tab_solicitacao_id_registro",
                table: "tab_solicitacao",
                column: "id_registro");

            migrationBuilder.CreateIndex(
                name: "IX_tab_solicitacao_user_id",
                table: "tab_solicitacao",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tab_banco_horas");

            migrationBuilder.DropTable(
                name: "tab_perfil");

            migrationBuilder.DropTable(
                name: "tab_solicitacao");

            migrationBuilder.DropTable(
                name: "tab_registro");

            migrationBuilder.DropTable(
                name: "tab_cadastro");
        }
    }
}
