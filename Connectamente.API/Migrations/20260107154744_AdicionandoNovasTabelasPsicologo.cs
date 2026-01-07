using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Connectamente.API.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoNovasTabelasPsicologo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AbordagemTerapeutica",
                columns: table => new
                {
                    IdAbordagemTerapeutica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbordagemTerapeutica", x => x.IdAbordagemTerapeutica);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CondicaoTerapeutica",
                columns: table => new
                {
                    IdCondicaoTerapeutica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CondicaoTerapeutica", x => x.IdCondicaoTerapeutica);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoPaciente",
                columns: table => new
                {
                    IdTipoPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPaciente", x => x.IdTipoPaciente);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sobrenome = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<DateOnly>(type: "date", nullable: false),
                    Foto = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QtdAcessos = table.Column<int>(type: "int", nullable: false),
                    TipoPerfil = table.Column<int>(type: "int", nullable: false),
                    PsicologoResponsavelId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Psicologo",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CRP = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ModalidadeDeAtendimento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psicologo", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Psicologo_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegistroPensamento",
                columns: table => new
                {
                    IdRegistro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CaminhoArquivoRegistro = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistroPensamento", x => x.IdRegistro);
                    table.ForeignKey(
                        name: "FK_RegistroPensamento_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PsicologoAbordagem",
                columns: table => new
                {
                    AbordagemTerapeuticaId = table.Column<int>(type: "int", nullable: false),
                    PsicologoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsicologoAbordagem", x => new { x.AbordagemTerapeuticaId, x.PsicologoId });
                    table.ForeignKey(
                        name: "FK_PsicologoAbordagem_AbordagemTerapeutica",
                        column: x => x.AbordagemTerapeuticaId,
                        principalTable: "AbordagemTerapeutica",
                        principalColumn: "IdAbordagemTerapeutica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsicologoAbordagem_Psicologo",
                        column: x => x.PsicologoId,
                        principalTable: "Psicologo",
                        principalColumn: "UsuarioId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PsicologoCondicaoTratada",
                columns: table => new
                {
                    CondicaoTerapeuticaId = table.Column<int>(type: "int", nullable: false),
                    PsicologoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsicologoCondicaoTratada", x => new { x.CondicaoTerapeuticaId, x.PsicologoId });
                    table.ForeignKey(
                        name: "FK_PsicologoCondicao_CondicaoTerapeutica",
                        column: x => x.CondicaoTerapeuticaId,
                        principalTable: "CondicaoTerapeutica",
                        principalColumn: "IdCondicaoTerapeutica",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsicologoCondicao_Psicologo",
                        column: x => x.PsicologoId,
                        principalTable: "Psicologo",
                        principalColumn: "UsuarioId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PsicologoTipoPaciente",
                columns: table => new
                {
                    PsicologoId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPacienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsicologoTipoPaciente", x => new { x.PsicologoId, x.TipoPacienteId });
                    table.ForeignKey(
                        name: "FK_PsicologoPaciente_Psicologo",
                        column: x => x.PsicologoId,
                        principalTable: "Psicologo",
                        principalColumn: "UsuarioId");
                    table.ForeignKey(
                        name: "FK_PsicologoPaciente_TipoPaciente",
                        column: x => x.TipoPacienteId,
                        principalTable: "TipoPaciente",
                        principalColumn: "IdTipoPaciente",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmocaoRegistro",
                columns: table => new
                {
                    IdEmocaoRegistro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Emocao = table.Column<int>(type: "int", nullable: false),
                    IntensidadeInicial = table.Column<int>(type: "int", nullable: false),
                    IntensidadeFinal = table.Column<int>(type: "int", nullable: false),
                    RegistroPensamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmocaoRegistro", x => x.IdEmocaoRegistro);
                    table.ForeignKey(
                        name: "FK_EmocaoRegistro_RegistroPensamento_RegistroPensamentoId",
                        column: x => x.RegistroPensamentoId,
                        principalTable: "RegistroPensamento",
                        principalColumn: "IdRegistro",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AbordagemTerapeutica",
                columns: new[] { "IdAbordagemTerapeutica", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Abordagem prática e focada em objetivos, que identifica e modifica padrões de pensamentos (cognições) e comportamentos negativos, sendo eficaz para ansiedade, depressão e TOC, com o terapeuta tendo um papel mais ativo.", "Terapia Cognitivo-Comportamental" },
                    { 2, "Baseada em Carl Rogers e Abraham Maslow, acredita na capacidade inata do indivíduo para o crescimento, focando na autoaceitação e realização do potencial humano, com o terapeuta oferecendo empatia e consideração positiva incondicional.", "Humanista/Centrada na Pessoa" },
                    { 3, "A psicoterapia fenomenológico-existencial é uma abordagem que combina a filosofia da fenomenologia e do existencialismo para compreender a experiência humana. ", "Psicoterapia Fenomenológico-Existencial" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", null, "Psicologo", "PSICOLOGO" },
                    { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", null, "Paciente", "PACIENTE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataNascimento", "Email", "EmailConfirmed", "Foto", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PsicologoResponsavelId", "QtdAcessos", "SecurityStamp", "Sobrenome", "TipoPerfil", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", 0, "68fb7d10-901a-4105-9534-38438cef41ef", new DateOnly(2002, 4, 1), "anajuliamattos02@gmail.com", true, "/img/usuarios/psicologo.png", true, null, "Ana Julia", "ANAJULIAMATTOS02@GMAIL.COM", "anajuliamattos02@GMAIL.COM", "AQAAAAIAAYagAAAAEE9M2YkpzA924MCFe1W5quROpOs9bE264K3UhvTAEmDpvm70TSJ0c+35JzXVmQha6w==", null, false, null, 0, "1ddfa952-0e5c-4124-9476-928b5be87413", " Reis de Mattos", 2, false, "anajuliamattos02@gmail.com" },
                    { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", 0, "17c5b11a-f065-436c-b4fd-da367614f4f0", new DateOnly(2001, 12, 19), "tainaravitsantos28@gmail.com", true, "/img/usuarios/paciente.png", true, null, "Tainara Vitoria", "TAINARAVITSANTOS28@GMAIL.COM", "tainaravitsantos28@GMAIL.COM", "AQAAAAIAAYagAAAAEDf1Z7YAFUr30edMYPDBUxY6Bwy6xjD6Vt2uRnDyou/bjwPTnUmHfjPu15yUfnWkbw==", null, false, null, 0, "2faf6c2d-71b6-4c2a-a233-70fe352e0fe5", " dos Santos", 1, false, "tainaravitsantos28@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "CondicaoTerapeutica",
                columns: new[] { "IdCondicaoTerapeutica", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Atendimento psicológico com foco nas vivências e desafios específicos da comunidade LGBTQIAPN+. O objetivo é oferecer um acolhimento livre de preconceitos, auxiliando em questões de aceitação, identidade de gênero, orientação sexual, além de fortalecer a autoestima e o enfrentamento de violências sociais.", "LGBTQIAPN+" },
                    { 2, "O luto é um processo natural diante de uma perda significativa, mas que pode ser extremamente doloroso e paralisante. A terapia oferece um espaço seguro para vivenciar as etapas do pesar, ajudando o paciente a ressignificar a perda e a encontrar formas de seguir em frente com a memória do que se foi.", "Luto" },
                    { 3, "A depressão vai além da tristeza profunda; é um transtorno que afeta o humor, a energia e o interesse pela vida. O acompanhamento terapêutico busca identificar as causas desses sentimentos, oferecer suporte emocional e desenvolver estratégias para recuperar a qualidade de vida e o bem-estar mental.", "Depressão" }
                });

            migrationBuilder.InsertData(
                table: "TipoPaciente",
                columns: new[] { "IdTipoPaciente", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "O atendimento infantil utiliza o brincar como a principal ferramenta de comunicação. Através da ludoterapia, o psicólogo auxilia a criança a expressar suas emoções, medos e conflitos, trabalhando questões comportamentais, dificuldades de aprendizagem e socialização em conjunto com a orientação aos pais ou responsáveis.", "Infantil+" },
                    { 2, "Focada na dinâmica do relacionamento, a terapia de casal busca mediar conflitos e melhorar a comunicação entre os parceiros. O objetivo é compreender os padrões de interação, fortalecer o vínculo afetivo ou auxiliar em processos de separação de forma saudável, proporcionando um espaço neutro de escuta e acolhimento para ambos.", "Casal" },
                    { 3, "A psicoterapia para adultos é um processo de autoconhecimento e cuidado com a saúde mental. Foca no enfrentamento de desafios cotidianos, como estresse, ansiedade, questões de carreira e relacionamentos, auxiliando o paciente a desenvolver recursos internos para lidar com suas emoções e tomar decisões mais conscientes e alinhadas aos seus valores.", "Adultos" }
                });

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

            migrationBuilder.InsertData(
                table: "PsicologoAbordagem",
                columns: new[] { "AbordagemTerapeuticaId", "PsicologoId" },
                values: new object[,]
                {
                    { 1, "0b44ca04-f6b0-4a8f-a953-1f2330d30894" },
                    { 2, "0b44ca04-f6b0-4a8f-a953-1f2330d30894" }
                });

            migrationBuilder.InsertData(
                table: "PsicologoCondicaoTratada",
                columns: new[] { "CondicaoTerapeuticaId", "PsicologoId" },
                values: new object[,]
                {
                    { 1, "0b44ca04-f6b0-4a8f-a953-1f2330d30894" },
                    { 2, "0b44ca04-f6b0-4a8f-a953-1f2330d30894" }
                });

            migrationBuilder.InsertData(
                table: "PsicologoTipoPaciente",
                columns: new[] { "PsicologoId", "TipoPacienteId" },
                values: new object[,]
                {
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", 1 },
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PsicologoResponsavelId",
                table: "AspNetUsers",
                column: "PsicologoResponsavelId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmocaoRegistro_RegistroPensamentoId",
                table: "EmocaoRegistro",
                column: "RegistroPensamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PsicologoAbordagem_PsicologoId",
                table: "PsicologoAbordagem",
                column: "PsicologoId");

            migrationBuilder.CreateIndex(
                name: "IX_PsicologoCondicaoTratada_PsicologoId",
                table: "PsicologoCondicaoTratada",
                column: "PsicologoId");

            migrationBuilder.CreateIndex(
                name: "IX_PsicologoTipoPaciente_TipoPacienteId",
                table: "PsicologoTipoPaciente",
                column: "TipoPacienteId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistroPensamento_UsuarioId",
                table: "RegistroPensamento",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Psicologo_PsicologoResponsavelId",
                table: "AspNetUsers",
                column: "PsicologoResponsavelId",
                principalTable: "Psicologo",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psicologo_AspNetUsers_UsuarioId",
                table: "Psicologo");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmocaoRegistro");

            migrationBuilder.DropTable(
                name: "PsicologoAbordagem");

            migrationBuilder.DropTable(
                name: "PsicologoCondicaoTratada");

            migrationBuilder.DropTable(
                name: "PsicologoTipoPaciente");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "RegistroPensamento");

            migrationBuilder.DropTable(
                name: "AbordagemTerapeutica");

            migrationBuilder.DropTable(
                name: "CondicaoTerapeutica");

            migrationBuilder.DropTable(
                name: "TipoPaciente");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Psicologo");
        }
    }
}
