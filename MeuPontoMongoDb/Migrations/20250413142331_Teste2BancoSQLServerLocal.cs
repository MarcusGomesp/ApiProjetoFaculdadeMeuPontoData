using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuPontoMongoDb.Migrations
{
    /// <inheritdoc />
    public partial class Teste2BancoSQLServerLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tab_perfil_tab_cadastro_UserId",
                table: "tab_perfil");

            migrationBuilder.RenameColumn(
                name: "UrlProfilePic",
                table: "tab_perfil",
                newName: "url_profile_pic");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tab_perfil",
                newName: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tab_perfil_tab_cadastro_user_id",
                table: "tab_perfil",
                column: "user_id",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tab_perfil_tab_cadastro_user_id",
                table: "tab_perfil");

            migrationBuilder.RenameColumn(
                name: "url_profile_pic",
                table: "tab_perfil",
                newName: "UrlProfilePic");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "tab_perfil",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tab_perfil_tab_cadastro_UserId",
                table: "tab_perfil",
                column: "UserId",
                principalTable: "tab_cadastro",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
