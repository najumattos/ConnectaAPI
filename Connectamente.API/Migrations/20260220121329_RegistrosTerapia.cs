using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Connectamente.API.Migrations
{
    /// <inheritdoc />
    public partial class RegistrosTerapia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RegistroConsultaModel",
                keyColumn: "RegistroSessaoId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "RegistroConsultaModel",
                keyColumn: "RegistroSessaoId",
                keyValue: -1);

            migrationBuilder.AddColumn<string>(
                name: "PsicologoId",
                table: "RegistroConsultaModel",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "EmocaoRegistro",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Emoji",
                table: "EmocaoRegistro",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroSessao_Psicologo_UsuarioId",
                table: "RegistroConsultaModel",
                column: "UsuarioId",
                principalTable: "PsicologoModel",
                principalColumn: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroSessao_Psicologo_UsuarioId",
                table: "RegistroConsultaModel");

            migrationBuilder.DropColumn(
                name: "PsicologoId",
                table: "RegistroConsultaModel");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "EmocaoRegistro");

            migrationBuilder.DropColumn(
                name: "Emoji",
                table: "EmocaoRegistro");

            migrationBuilder.InsertData(
                table: "RegistroConsultaModel",
                columns: new[] { "RegistroSessaoId", "DataHoraSessao", "DuracaoSessao", "PacienteId", "ResumoSessao", "UsuarioId" },
                values: new object[,]
                {
                    { -2, new DateTime(2026, 5, 1, 16, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(50), "59de1fac-5ba6-49b0-8849-c97e3c7ba11b", "Resumo Sessao 2", null },
                    { -1, new DateTime(2026, 4, 1, 16, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(50), "59de1fac-5ba6-49b0-8849-c97e3c7ba11b", "Resumo Sessao 1", null }
                });
        }
    }
}
