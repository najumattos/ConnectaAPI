using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectamente.API.Migrations
{
    /// <inheritdoc />
    public partial class NovaMigration0903 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PsicologoModel",
                keyColumn: "UsuarioId",
                keyValue: "70f93f27-32b1-4de5-bee3-b0de2cf80047",
                column: "ModalidadeDeAtendimento",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PsicologoModel",
                keyColumn: "UsuarioId",
                keyValue: "70f93f27-32b1-4de5-bee3-b0de2cf80047",
                column: "ModalidadeDeAtendimento",
                value: 1);
        }
    }
}
