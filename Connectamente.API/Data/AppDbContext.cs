using Connectamente.API.Data.Configurations;
using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Connectamente.API.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UsuarioModel>(options)
{
    public DbSet<PsicologoModel> Psicologos { get; set; }
    public DbSet<PacienteModel> Pacientes { get; set; }
    public DbSet<UsuarioModel> Usuarios { get; set; }
    public DbSet<ConsultaModel> Consultas { get; set; }
    public DbSet<ProntuarioModel> Prontuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        PopulateRoles(builder);
        builder.ApplyConfiguration(new UserConfig());
        builder.ApplyConfiguration(new ProntuarioConfig());
        builder.ApplyConfiguration(new PacienteConfig());
        builder.ApplyConfiguration(new ConsultaConfig());
        builder.ApplyConfiguration(new PsicologoConfig());
    }

    private static void PopulateRoles(ModelBuilder builder)
    {     
        List<IdentityRole> roles =
        [
            new IdentityRole() {
               Id = SeedDataConstants.USER_ESTUDANTE_ID,
               Name = "Estudante",
               NormalizedName = "ESTUDANTE"
            },
                       new IdentityRole() {
               Id = SeedDataConstants.USER_COORDENADOR_ID,
               Name = "Coordendor",
               NormalizedName = "COORDENADOR"
            },
            new IdentityRole() {
               Id = SeedDataConstants.ROLE_CLINICA_ID,
               Name = "Clinica",
               NormalizedName = "CLINICA"
            },
        ];
        builder.Entity<IdentityRole>().HasData(roles);
      
        #region Populate UserRole - Usuário com Perfil
        List<IdentityUserRole<string>> userRoles =
        [
            new IdentityUserRole<string>() {
                UserId =SeedDataConstants.USER_ESTUDANTE_ID,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = SeedDataConstants.USER_COORDENADOR_ID,
                RoleId = roles[1].Id
            },
            new IdentityUserRole<string>() {
                UserId = SeedDataConstants.USER_CLINICA_ID,
                RoleId = roles[2].Id
            }
        ];
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }   
}