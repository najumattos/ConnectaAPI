using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Connectamente.API.Migrations
{
    /// <inheritdoc />
    public partial class AdcSeedDataConstants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", "0b44ca04-f6b0-4a8f-a953-1f2330d30894" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", "ddf093a6-6cb5-4ff7-9a64-83da34aee005" });

            migrationBuilder.DeleteData(
                table: "Psicologo",
                keyColumn: "UsuarioId",
                keyValue: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ddf093a6-6cb5-4ff7-9a64-83da34aee005");

            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -2,
                column: "PsicologoId",
                value: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -1,
                column: "PsicologoId",
                value: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataNascimento", "Email", "EmailConfirmed", "Foto", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PsicologoResponsavelId", "QtdAcessos", "SecurityStamp", "Sobrenome", "TipoPerfil", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "59de1fac-5ba6-49b0-8849-c97e3c7ba11b", 0, "7cb3541f-0085-4145-b6d7-3e9ae3a300a5", new DateOnly(2001, 12, 19), "tainaravitsantos28@gmail.com", true, "/img/usuarios/paciente.png", true, null, "Tainara Vitoria", "TAINARAVITSANTOS28@GMAIL.COM", "TAINARAVITSANTOS28@GMAIL.COM", "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==", null, false, null, 0, "5b0faad3-6502-4325-94ee-33aab11905d7", " dos Santos", 1, false, "tainaravitsantos28@gmail.com" },
                    { "70f93f27-32b1-4de5-bee3-b0de2cf80047", 0, "5458aee0-71ca-4f08-88e8-0f03d18d6960", new DateOnly(2002, 4, 1), "anajuliamattos02@gmail.com", true, "/img/usuarios/psicologo.png", true, null, "Ana Julia", "ANAJULIAMATTOS02@GMAIL.COM", "ANAJULIAMATTOS02@GMAIL.COM", "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==", null, false, null, 0, "15cfe30f-1dac-404e-85e6-02159dbed489", " Reis de Mattos", 2, false, "anajuliamattos02@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -2,
                column: "PsicologoId",
                value: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -1,
                column: "PsicologoId",
                value: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.UpdateData(
                table: "PacientePsicologo",
                keyColumn: "PacientePsicologoId",
                keyValue: -2,
                column: "PsicologoId",
                value: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.UpdateData(
                table: "PacientePsicologo",
                keyColumn: "PacientePsicologoId",
                keyValue: -1,
                column: "PsicologoId",
                value: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", "59de1fac-5ba6-49b0-8849-c97e3c7ba11b" },
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", "70f93f27-32b1-4de5-bee3-b0de2cf80047" }
                });

            migrationBuilder.InsertData(
                table: "Psicologo",
                columns: new[] { "UsuarioId", "CRP", "Descricao", "ModalidadeDeAtendimento" },
                values: new object[] { "70f93f27-32b1-4de5-bee3-b0de2cf80047", "12345", "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental.", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", "59de1fac-5ba6-49b0-8849-c97e3c7ba11b" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", "70f93f27-32b1-4de5-bee3-b0de2cf80047" });

            migrationBuilder.DeleteData(
                table: "Psicologo",
                keyColumn: "UsuarioId",
                keyValue: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "59de1fac-5ba6-49b0-8849-c97e3c7ba11b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70f93f27-32b1-4de5-bee3-b0de2cf80047");

            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -2,
                column: "PsicologoId",
                value: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.UpdateData(
                table: "AbordagemPsicologo",
                keyColumn: "AbordagemPsicologoId",
                keyValue: -1,
                column: "PsicologoId",
                value: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataNascimento", "Email", "EmailConfirmed", "Foto", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PsicologoResponsavelId", "QtdAcessos", "SecurityStamp", "Sobrenome", "TipoPerfil", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", 0, "5458aee0-71ca-4f08-88e8-0f03d18d6960", new DateOnly(2002, 4, 1), "anajuliamattos02@gmail.com", true, "/img/usuarios/psicologo.png", true, null, "Ana Julia", "ANAJULIAMATTOS02@GMAIL.COM", "ANAJULIAMATTOS02@GMAIL.COM", "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==", null, false, null, 0, "15cfe30f-1dac-404e-85e6-02159dbed489", " Reis de Mattos", 2, false, "anajuliamattos02@gmail.com" },
                    { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", 0, "7cb3541f-0085-4145-b6d7-3e9ae3a300a5", new DateOnly(2001, 12, 19), "tainaravitsantos28@gmail.com", true, "/img/usuarios/paciente.png", true, null, "Tainara Vitoria", "TAINARAVITSANTOS28@GMAIL.COM", "TAINARAVITSANTOS28@GMAIL.COM", "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==", null, false, null, 0, "5b0faad3-6502-4325-94ee-33aab11905d7", " dos Santos", 1, false, "tainaravitsantos28@gmail.com" }
                });

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -2,
                column: "PsicologoId",
                value: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.UpdateData(
                table: "CondicaoPsicologo",
                keyColumn: "CondicaoPsicologoId",
                keyValue: -1,
                column: "PsicologoId",
                value: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.UpdateData(
                table: "PacientePsicologo",
                keyColumn: "PacientePsicologoId",
                keyValue: -2,
                column: "PsicologoId",
                value: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.UpdateData(
                table: "PacientePsicologo",
                keyColumn: "PacientePsicologoId",
                keyValue: -1,
                column: "PsicologoId",
                value: "0b44ca04-f6b0-4a8f-a953-1f2330d30894");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", "0b44ca04-f6b0-4a8f-a953-1f2330d30894" },
                    { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", "ddf093a6-6cb5-4ff7-9a64-83da34aee005" }
                });

            migrationBuilder.InsertData(
                table: "Psicologo",
                columns: new[] { "UsuarioId", "CRP", "Descricao", "ModalidadeDeAtendimento" },
                values: new object[] { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", "12345", "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental.", 1 });
        }
    }
}
