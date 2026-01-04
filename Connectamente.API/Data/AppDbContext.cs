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
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Psicologo> Psicologos { get; set; }
    public DbSet<AbordagemTerapeutica> AbordagensTerapeuticas { get; set; }
    public DbSet<EmocaoRegistro> EmocoesRegistro { get; set; }
    public DbSet<RegistroPensamento> RegistroPensamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedUsuarioPadrao(builder);
        SeedAbordagemPadrao(builder);
        SeedPsicologoPadrao(builder);

        //IA
        // Configuração para o relacionamento muitos-para-muitos entre Psicologo e AbordagemTerapeutica
        builder.Entity<Psicologo>()
     .HasMany(p => p.AbordagensTerapeuticas)
     .WithMany() // Deixe vazio se a classe AbordagemTerapeutica não tiver uma ICollection<Psicologo>
     .UsingEntity<Dictionary<string, object>>(
         "PsicologoAbordagem",
         j => j
             .HasOne<AbordagemTerapeutica>()
             .WithMany()
             .HasForeignKey("AbordagemTerapeuticaId") // Verifique se no banco é int ou uint
             .HasConstraintName("FK_PsicologoAbordagem_AbordagemTerapeutica")
             .OnDelete(DeleteBehavior.Cascade),
         j => j
             .HasOne<Psicologo>()
             .WithMany()
             .HasForeignKey("PsicologoId")
             .HasConstraintName("FK_PsicologoAbordagem_Psicologo")
             .OnDelete(DeleteBehavior.ClientCascade)
     );

    }
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

        #region Populate Usuário
        List<Usuario> usuarios = [
            new Usuario(){
                Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
                Email = "anajuliamattos02@gmail.com",
                NormalizedEmail = "ANAJULIAMATTOS02@GMAIL.COM",
                UserName = "anajuliamattos02@gmail.com",
                NormalizedUserName = "anajuliamattos02@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Ana Julia",
                Sobrenome = " Reis de Mattos",                
                DataNascimento = DateTime.Parse("01/04/2002"),
                Foto = "/img/usuarios/psicologo.png",
                TipoPerfil = Enums.TipoPerfil.Psicologo
            },
             new Usuario(){
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "tainaravitsantos28@gmail.com",
                NormalizedEmail = "TAINARAVITSANTOS28@GMAIL.COM",
                UserName = "tainaravitsantos28@gmail.com",
                NormalizedUserName = "tainaravitsantos28@GMAIL.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Tainara Vitoria",
                Sobrenome = " dos Santos",
                DataNascimento = DateTime.Parse("19/12/2001"),
                Foto = "/img/usuarios/paciente.png"
            }
        ];
            PasswordHasher<Usuario> pass = new();
        foreach (var user in usuarios)
        {
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
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

    private static void SeedAbordagemPadrao(ModelBuilder builder)
    {
        List<AbordagemTerapeutica> abordagensTerapeutica =
        [
            new()
            {
             IdAbordagemTerapeutica = 1,
             Nome = "Terapia Cognitivo-Comportamental",
             Descricao = "Abordagem prática e focada em objetivos, que identifica e modifica padrões de pensamentos (cognições) e comportamentos negativos, sendo eficaz para ansiedade, depressão e TOC, com o terapeuta tendo um papel mais ativo."
            },
            new()
            {
             IdAbordagemTerapeutica = 2,
             Nome = "Humanista/Centrada na Pessoa",
             Descricao = "Baseada em Carl Rogers e Abraham Maslow, acredita na capacidade inata do indivíduo para o crescimento, focando na autoaceitação e realização do potencial humano, com o terapeuta oferecendo empatia e consideração positiva incondicional."
            },
            new()
            {
             IdAbordagemTerapeutica = 3,
             Nome = "Psicoterapia Fenomenológico-Existencial",
             Descricao = "A psicoterapia fenomenológico-existencial é uma abordagem que combina a filosofia da fenomenologia e do existencialismo para compreender a experiência humana. "
            }
        ];
        builder.Entity<AbordagemTerapeutica>().HasData(abordagensTerapeutica);
    }
    private static void SeedPsicologoPadrao(ModelBuilder builder)
    {
        string psicologoId = "0b44ca04-f6b0-4a8f-a953-1f2330d30894";
        List<Psicologo> psicologos = 
       [
            new(){
                UsuarioId = psicologoId,
                CRP = "12345",
                Descricao = "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental.",
                ModalidadeDeAtendimento = Enums.ModalidadeAtendimento.Presencial                 
            }

            ];
        builder.Entity<Psicologo>().HasData(psicologos);
        builder.Entity("PsicologoAbordagem").HasData(        
            new { PsicologoId = psicologoId, AbordagemTerapeuticaId = 1u },
            new { PsicologoId = psicologoId, AbordagemTerapeuticaId = 2u }
        );
    }

}
