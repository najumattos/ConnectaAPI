using Connectamente.API.Data.Configurations;
using Connectamente.API.Helpers;
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
        PopulateRoles(builder);
        builder.ApplyConfiguration(new UserConfiguration());

        SeedPsicologoPadrao(builder);
        SeedAbordagemPsicologoPadrao(builder);
        SeedCondicaoPsicologoPadrao(builder);
        SeedPacientePsicologoPadrao(builder);
    }

    private static void SeedAbordagemPsicologoPadrao(ModelBuilder builder)
    {      
        builder.Entity<AbordagemPsicologo>().HasData(
            new AbordagemPsicologo
            {
                AbordagemPsicologoId = -1,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                AbordagemTerapeutica = Enums.AbordagemTerapeutica.TCC
            },
            new AbordagemPsicologo
            {
                AbordagemPsicologoId = -2,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                AbordagemTerapeutica = Enums.AbordagemTerapeutica.Psicanalise
            }
        );
    }
    private static void SeedCondicaoPsicologoPadrao(ModelBuilder builder)
    {        
        builder.Entity<CondicaoPsicologo>().HasData(
            new CondicaoPsicologo
            {
                CondicaoPsicologoId = -1,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                CondicaoTerapeutica = Enums.CondicaoTerapeutica.Gestacao
            },
            new CondicaoPsicologo
            {
                CondicaoPsicologoId = -2,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                CondicaoTerapeutica = Enums.CondicaoTerapeutica.FobiaEspecifica
            }
        );
    }
   private static void SeedPacientePsicologoPadrao(ModelBuilder builder)
    {       
        builder.Entity<PacientePsicologo>().HasData(
            new PacientePsicologo
            {
                PacientePsicologoId = -1,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                TipoPaciente = Enums.TipoPaciente.Familiar
            },
            new PacientePsicologo
            {
                PacientePsicologoId = -2,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                TipoPaciente = Enums.TipoPaciente.Geriatrico
            }
        );
    }
    private static void SeedPsicologoPadrao(ModelBuilder builder)
    {
        builder.Entity<Psicologo>().HasData(
        new Psicologo
        {
            UsuarioId = SeedDataConstants.USER_ANA_JULIA_ID,
            CRP = "12345",
            Descricao = "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental.",
            ModalidadeDeAtendimento = Enums.ModalidadeAtendimento.Presencial
        });

        //falta EmocaoRegistro() e RegistroPensamento()
    }
    private static void PopulateRoles(ModelBuilder builder)
    {     
        List<IdentityRole> roles =
        [
            new IdentityRole() {
               Id = SeedDataConstants.ROLE_PSICOLOGO_ID,
               Name = "Psicologo",
               NormalizedName = "PSICOLOGO"
            },
            new IdentityRole() {
               Id = SeedDataConstants.ROLE_PACIENTE_ID,
               Name = "Paciente",
               NormalizedName = "PACIENTE"
            },
        ];
        builder.Entity<IdentityRole>().HasData(roles);
      
        #region Populate UserRole - Usuário com Perfil
        List<IdentityUserRole<string>> userRoles =
        [
            new IdentityUserRole<string>() {
                UserId =SeedDataConstants.USER_ANA_JULIA_ID,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = SeedDataConstants.USER_TAINARA_ID,
                RoleId = roles[1].Id
            }
        ];
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }
}