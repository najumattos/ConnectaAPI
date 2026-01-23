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
        builder.ApplyConfiguration(new PsicoConfigurations());
        builder.ApplyConfiguration(new PacienteConfig());
        builder.ApplyConfiguration(new AbordagemConfig());
        builder.ApplyConfiguration(new CondicaoConfig());
        builder.ApplyConfiguration(new TipoPacienteConfig());
        CascataConfigPsico(builder);

    }

    private static void CascataConfigPsico(ModelBuilder builder)
    {
        builder.Entity<AbordagemPsicologo>()
            .HasOne(a => a.Psicologo)
            .WithMany(p => p.AbordagensTerapeuticas)
            .HasForeignKey(a => a.PsicologoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CondicaoPsicologo>()
            .HasOne(a => a.Psicologo)
            .WithMany(p => p.CondicoesTerapeuticas)
            .HasForeignKey(a => a.PsicologoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PacientePsicologo>()
           .HasOne(a => a.Psicologo)
           .WithMany(p => p.TiposPacientes)
           .HasForeignKey(a => a.PsicologoId)
           .OnDelete(DeleteBehavior.Cascade);
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