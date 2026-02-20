using Connectamente.API.Data.Configurations;
using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models.PsicologoModel;
using Connectamente.API.Models.RPD;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Connectamente.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<Usuario>(options)
{
   public DbSet<EmocaoRegistro> EmocoesRegistro { get; set; }
    public DbSet<Psicologo> Psicologos { get; set; }
    public DbSet<RegistroPensamento> RegistroPensamentos { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<RegistroSessao> RegistrosSessoes { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        PopulateRoles(builder);
        builder.ApplyConfiguration(new UserConfig());

        builder.ApplyConfiguration(new PacienteConfig());
        builder.ApplyConfiguration(new RegistroSessaoTerapeuticaConfig());

        builder.ApplyConfiguration(new PsicoConfig());
        builder.Entity<Psicologo>()
        .Property(p => p.AbordagensTerapeuticas)
        .HasConversion(
            v => string.Join(',', v.Select(e => (int)e)), // Salva como "1,2,3"
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                  .Select(val => (AbordagemTerapeutica)int.Parse(val)).ToList() // Volta como Lista
        );
        builder.Entity<Psicologo>()
       .Property(p => p.CondicoesTerapeuticas)
       .HasConversion(
           v => string.Join(',', v.Select(e => (int)e)), // Salva como "1,2,3"
           v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                 .Select(val => (CondicaoTerapeutica)int.Parse(val)).ToList() // Volta como Lista
       );
        builder.Entity<Psicologo>()
       .Property(p => p.TiposPacientes)
       .HasConversion(
           v => string.Join(',', v.Select(e => (int)e)), // Salva como "1,2,3"
           v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                 .Select(val => (TipoPaciente)int.Parse(val)).ToList() // Volta como Lista
       );

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