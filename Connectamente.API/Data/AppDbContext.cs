using Connectamente.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Data;

public class AppDbContext : IdentityDbContext<Usuario>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<AbordagemPsicologo> AbordagensPsicologo { get; set; }
    public DbSet<CondicaoPsicologo> CondicoesTerapeuticas { get; set; }
    public DbSet<EmocaoRegistro> EmocoesRegistro { get; set; }
    public DbSet<Psicologo> Psicologos { get; set; }
    public DbSet<RegistroPensamento> RegistroPensamentos { get; set; }
    public DbSet<PacientePsicologo> TiposPaciente { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedAbordagemPsicologoPadrao(builder);
        SeedCondicaoPsicologoPadrao(builder);
        //SeedEmocaoRegistroPadrao(builder);
        SeedPacientePsicologoPadrao(builder);
        SeedPsicologoPadrao(builder);
        //SeedRegistroPensamentoPadrao(builder)
        SeedUsuarioPadrao(builder);

        //IA
        // Configuração para o relacionamento 1-N entre Psicologo e AbordagemPsicologo
        builder.Entity<Psicologo>()
      .HasMany(p => p.AbordagensTerapeuticas)
      .WithOne(a => a.Psicologo)
      .HasForeignKey(a => a.PsicologoId);

        // Configuração para o relacionamento 1-N entre Psicologo e CondicaoPsicologo
        builder.Entity<Psicologo>()
      .HasMany(p => p.CondicoesTerapeuticas)
      .WithOne(c => c.Psicologo)
      .HasForeignKey(c => c.PsicologoId);

        // Configuração para o relacionamento 1-N entre Psicologo e PacientePsicologo
        builder.Entity<Psicologo>()
      .HasMany(p => p.TiposPacientes)
      .WithOne(c => c.Psicologo)
      .HasForeignKey(c => c.PsicologoId);
        // Relacionamento Paciente -> Psicologo Responsável
        builder.Entity<Usuario>()
            .HasOne(u => u.PsicologoResponsavel)
            .WithMany()
            .HasForeignKey(u => u.PsicologoResponsavelId);
           // .OnDelete(DeleteBehavior.); tem qe ver isso ae
        //falta EmocaoRegistro() e RegistroPensamento()
    }

    private static void SeedAbordagemPsicologoPadrao(ModelBuilder builder) {
        string psicologoId = "0b44ca04-f6b0-4a8f-a953-1f2330d30894";

        builder.Entity<AbordagemPsicologo>().HasData(
            new AbordagemPsicologo
            {
                AbordagemPsicologoId = -1,
                PsicologoId = psicologoId,
                AbordagemTerapeutica = Enums.AbordagemTerapeutica.TCC
            },
            new AbordagemPsicologo
            {
                AbordagemPsicologoId = -2,
                PsicologoId = psicologoId,
                AbordagemTerapeutica = Enums.AbordagemTerapeutica.Psicanalise
            }
        );
    }
    private static void SeedCondicaoPsicologoPadrao(ModelBuilder builder) {
        string psicologoId = "0b44ca04-f6b0-4a8f-a953-1f2330d30894";

        builder.Entity<CondicaoPsicologo>().HasData(
            new CondicaoPsicologo
            {
                CondicaoPsicologoId = -1,
                PsicologoId = psicologoId,
                CondicaoTerapeutica = Enums.CondicaoTerapeutica.Gestacao
            },
            new CondicaoPsicologo
            {
                CondicaoPsicologoId = -2,
                PsicologoId = psicologoId,
                CondicaoTerapeutica = Enums.CondicaoTerapeutica.FobiaEspecifica
            }
        );
    }
    /*private static void SeedEmocaoRegistroPadrao(ModelBuilder builder) { }*/
    private static void SeedPacientePsicologoPadrao(ModelBuilder builder) {
        string psicologoId = "0b44ca04-f6b0-4a8f-a953-1f2330d30894";

        builder.Entity<PacientePsicologo>().HasData(
            new PacientePsicologo
            {
                PacientePsicologoId = -1,
                PsicologoId = psicologoId,
                TipoPaciente = Enums.TipoPaciente.Familiar
            },
            new PacientePsicologo
            {
                PacientePsicologoId = -2,
                PsicologoId = psicologoId,
                TipoPaciente = Enums.TipoPaciente.Geriatrico
            }
        );
    }
    private static void SeedPsicologoPadrao(ModelBuilder builder)
    {
        string psicologoId = "0b44ca04-f6b0-4a8f-a953-1f2330d30894";
        builder.Entity<Psicologo>().HasData(
        new Psicologo{
                UsuarioId = psicologoId,
                CRP = "12345",
                Descricao = "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental.",
                ModalidadeDeAtendimento = Enums.ModalidadeAtendimento.Presencial
            });              
       
        //falta EmocaoRegistro() e RegistroPensamento()
    }
    /*private static void SeedRegistroPensamentoPadrao(ModelBuilder builder) { }*/
    private static void SeedUsuarioPadrao(ModelBuilder builder)
    {
        #region Populate Roles - Perfis de Usuário
        List<IdentityRole> roles =
        [
            new IdentityRole() {
               Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
               Name = "Psicologo",
               NormalizedName = "PSICOLOGO"
            },
            new IdentityRole() {
               Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
               Name = "Paciente",
               NormalizedName = "PACIENTE"
            },
        ];
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion
        
        string hashFixo = "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==";
        #region Populate Usuário
        List<Usuario> usuarios = [
            new Usuario(){
                Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
                Email = "anajuliamattos02@gmail.com",
                NormalizedEmail = "ANAJULIAMATTOS02@GMAIL.COM",
                UserName = "anajuliamattos02@gmail.com",
                NormalizedUserName = "ANAJULIAMATTOS02@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Ana Julia",
                Sobrenome = " Reis de Mattos",
                DataNascimento = new DateOnly(2002, 4, 1),
                Foto = "/img/usuarios/psicologo.png",
                TipoPerfil = Enums.TipoPerfil.Psicologo,
                PasswordHash = hashFixo,
                SecurityStamp = "55952B9E-D8B4-46E0-9E1A-D790177726D6", // Valor fixo qualquer
    ConcurrencyStamp = "867D9C11-C732-4740-953B-99763567BB45"
            },
             new Usuario(){
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "tainaravitsantos28@gmail.com",
                NormalizedEmail = "TAINARAVITSANTOS28@GMAIL.COM",
                UserName = "tainaravitsantos28@gmail.com",
                NormalizedUserName = "TAINARAVITSANTOS28@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Tainara Vitoria",
                Sobrenome = " dos Santos",
                DataNascimento = new DateOnly(2001, 12, 19),
                Foto = "/img/usuarios/paciente.png",
                PasswordHash = hashFixo,
                SecurityStamp = "B06D441D-A7B0-4A9B-983D-4A47008C369B",
                 ConcurrencyStamp = "F1A3E7E1-8812-4C6E-8C8B-885521C55355"
            }
        ];

       
        builder.Entity<Usuario>().HasData(usuarios);
        #endregion

        #region Populate UserRole - Usuário com Perfil
        List<IdentityUserRole<string>> userRoles =
        [
            new IdentityUserRole<string>() {
                UserId = usuarios[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = usuarios[1].Id,
                RoleId = roles[1].Id
            }
        ];
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }


}
