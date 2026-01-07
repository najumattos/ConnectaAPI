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
    public DbSet<AbordagemTerapeutica> AbordagensTerapeuticas { get; set; }
    public DbSet<CondicaoTerapeutica> CondicoesTerapeuticas { get; set; }
    public DbSet<EmocaoRegistro> EmocoesRegistro { get; set; }
    public DbSet<Psicologo> Psicologos { get; set; }
    public DbSet<RegistroPensamento> RegistroPensamentos { get; set; }
    public DbSet<TipoPaciente> TiposPaciente { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedAbordagemPadrao(builder);
        SeedCondicaoPadrao(builder);
        SeedTipoPacientePadrao(builder);
        SeedUsuarioPadrao(builder);
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
             .HasForeignKey("AbordagemTerapeuticaId")
             .HasConstraintName("FK_PsicologoAbordagem_AbordagemTerapeutica")
             .OnDelete(DeleteBehavior.Cascade),
         j => j
             .HasOne<Psicologo>()
             .WithMany()
             .HasForeignKey("PsicologoId")
             .HasConstraintName("FK_PsicologoAbordagem_Psicologo")
             .OnDelete(DeleteBehavior.ClientCascade)
     );
        
        // Configuração para o relacionamento muitos-para-muitos entre Psicologo e CondicaoTerapeutica
        builder.Entity<Psicologo>()
     .HasMany(p => p.CondicoesTerapeuticas)
     .WithMany() 
     .UsingEntity<Dictionary<string, object>>(
         "PsicologoCondicaoTratada",
         j => j
             .HasOne<CondicaoTerapeutica>()
             .WithMany()
             .HasForeignKey("CondicaoTerapeuticaId")
             .HasConstraintName("FK_PsicologoCondicao_CondicaoTerapeutica")
             .OnDelete(DeleteBehavior.Cascade),
         j => j
             .HasOne<Psicologo>()
             .WithMany()
             .HasForeignKey("PsicologoId")
             .HasConstraintName("FK_PsicologoCondicao_Psicologo")
             .OnDelete(DeleteBehavior.ClientCascade)
     );
        
        // Configuração para o relacionamento muitos-para-muitos entre Psicologo e TipoPaciente
        builder.Entity<Psicologo>()
     .HasMany(p => p.TipoPaciente)
     .WithMany() 
     .UsingEntity<Dictionary<string, object>>(
         "PsicologoTipoPaciente",
         j => j
             .HasOne<TipoPaciente>()
             .WithMany()
             .HasForeignKey("TipoPacienteId")
             .HasConstraintName("FK_PsicologoPaciente_TipoPaciente")
             .OnDelete(DeleteBehavior.Cascade),
         j => j
             .HasOne<Psicologo>()
             .WithMany()
             .HasForeignKey("PsicologoId")
             .HasConstraintName("FK_PsicologoPaciente_Psicologo")
             .OnDelete(DeleteBehavior.ClientCascade)
     );
        builder.Entity<Usuario>()
    .HasOne(u => u.PsicologoResponsavel)
    .WithMany()
    .HasForeignKey(u => u.PsicologoResponsavelId)
    .OnDelete(DeleteBehavior.Restrict);
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
                DataNascimento = DateOnly.Parse("01/04/2002"),
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
                DataNascimento = DateOnly.Parse("19/12/2001"),
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

    private static void SeedCondicaoPadrao(ModelBuilder builder)
    {
        List<CondicaoTerapeutica> condicoesTerapeuticas =
        [
            new()
            {
             IdCondicaoTerapeutica = 1,
             Nome = "LGBTQIAPN+",
             Descricao = "Atendimento psicológico com foco nas vivências e desafios específicos da comunidade LGBTQIAPN+. O objetivo é oferecer um acolhimento livre de preconceitos, auxiliando em questões de aceitação, identidade de gênero, orientação sexual, além de fortalecer a autoestima e o enfrentamento de violências sociais."
            },
            new()
            {
             IdCondicaoTerapeutica = 2,
             Nome = "Luto",
             Descricao = "O luto é um processo natural diante de uma perda significativa, mas que pode ser extremamente doloroso e paralisante. A terapia oferece um espaço seguro para vivenciar as etapas do pesar, ajudando o paciente a ressignificar a perda e a encontrar formas de seguir em frente com a memória do que se foi."
            },
            new()
            {
             IdCondicaoTerapeutica = 3,
             Nome = "Depressão",
             Descricao = "A depressão vai além da tristeza profunda; é um transtorno que afeta o humor, a energia e o interesse pela vida. O acompanhamento terapêutico busca identificar as causas desses sentimentos, oferecer suporte emocional e desenvolver estratégias para recuperar a qualidade de vida e o bem-estar mental."
            }
        ];
        builder.Entity<CondicaoTerapeutica>().HasData(condicoesTerapeuticas);
    }

    private static void SeedTipoPacientePadrao(ModelBuilder builder)
    {
        List<TipoPaciente> tipoPacientesTratados =
        [
            new()
            {
             IdTipoPaciente = 1,
             Nome = "Infantil+",
             Descricao = "O atendimento infantil utiliza o brincar como a principal ferramenta de comunicação. Através da ludoterapia, o psicólogo auxilia a criança a expressar suas emoções, medos e conflitos, trabalhando questões comportamentais, dificuldades de aprendizagem e socialização em conjunto com a orientação aos pais ou responsáveis."
            },
            new()
            {
             IdTipoPaciente = 2,
             Nome = "Casal",
             Descricao = "Focada na dinâmica do relacionamento, a terapia de casal busca mediar conflitos e melhorar a comunicação entre os parceiros. O objetivo é compreender os padrões de interação, fortalecer o vínculo afetivo ou auxiliar em processos de separação de forma saudável, proporcionando um espaço neutro de escuta e acolhimento para ambos."
            },
            new()
            {
             IdTipoPaciente = 3,
             Nome = "Adultos",
             Descricao = "A psicoterapia para adultos é um processo de autoconhecimento e cuidado com a saúde mental. Foca no enfrentamento de desafios cotidianos, como estresse, ansiedade, questões de carreira e relacionamentos, auxiliando o paciente a desenvolver recursos internos para lidar com suas emoções e tomar decisões mais conscientes e alinhadas aos seus valores."
            }
        ];
        builder.Entity<TipoPaciente>().HasData(tipoPacientesTratados);
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
            new { PsicologoId = psicologoId, AbordagemTerapeuticaId = 1 },
            new { PsicologoId = psicologoId, AbordagemTerapeuticaId = 2 }
        );

        builder.Entity("PsicologoCondicaoTratada").HasData(
            new { PsicologoId = psicologoId, CondicaoTerapeuticaId = 1 }, 
            new { PsicologoId = psicologoId, CondicaoTerapeuticaId = 2 }  
        );

        builder.Entity("PsicologoTipoPaciente").HasData(
            new { PsicologoId = psicologoId, TipoPacienteId = 1 }, 
            new { PsicologoId = psicologoId, TipoPacienteId = 2 }  
        );
    }

}
