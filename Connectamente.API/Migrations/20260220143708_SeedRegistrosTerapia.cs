using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Connectamente.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedRegistrosTerapia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RegistroSessao",
                columns: new[] { "RegistroSessaoId", "DataHoraSessao", "DuracaoSessao", "PacienteId", "PsicologoId", "ResumoSessao", "UsuarioId" },
                values: new object[] { -1, new DateTime(2002, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 50, 0, 0), "59de1fac-5ba6-49b0-8849-c97e3c7ba11b", "70f93f27-32b1-4de5-bee3-b0de2cf80047", "Resumo sessao", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegistroSessao",
                keyColumn: "RegistroSessaoId",
                keyValue: -1);
        }
    }
}
