using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectamente.API.Migrations
{
    /// <inheritdoc />
    public partial class DefinindoValoresPadrao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -2,
                column: "AbordagemTerapeutica",
                value: 2);

            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -1,
                column: "AbordagemTerapeutica",
                value: 1);

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -2,
                column: "CondicaoTerapeutica",
                value: 14);

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -1,
                column: "CondicaoTerapeutica",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -2,
                column: "AbordagemTerapeutica",
                value: 1);

            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -1,
                column: "AbordagemTerapeutica",
                value: 0);

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -2,
                column: "CondicaoTerapeutica",
                value: 13);

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -1,
                column: "CondicaoTerapeutica",
                value: 3);
        }
    }
}
