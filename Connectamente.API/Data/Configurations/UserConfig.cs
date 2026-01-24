using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
        {
           
            string hashFixo = "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==";
            #region Populate Usuário
            List<Usuario> usuarios = [
                new Usuario(){
                Id = SeedDataConstants.USER_ANA_JULIA_ID,
                UserName = "anajuliamattos02@gmail.com",
                Email = "anajuliamattos02@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "ANAJULIAMATTOS02@GMAIL.COM",
                Nome = "Ana Julia",
                Sobrenome = " Reis de Mattos",
                PhoneNumber = "14920044824",
                DataNascimento = new DateOnly(2002, 4, 1),
                Foto = "/img/usuarios/psicologo.png",
                TipoPerfil = Enums.TipoPerfil.Psicologo,
                NormalizedUserName = "ANAJULIAMATTOS02@GMAIL.COM",
                LockoutEnabled = true,
                PasswordHash = hashFixo,
                SecurityStamp = "15cfe30f-1dac-404e-85e6-02159dbed489",
                ConcurrencyStamp = "5458aee0-71ca-4f08-88e8-0f03d18d6960",
                AccessFailedCount = 0 ,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            },
             new Usuario(){
                Id = SeedDataConstants.USER_TAINARA_ID,
                UserName = "tainaravitsantos28@gmail.com",
                Email = "tainaravitsantos28@gmail.com",
                EmailConfirmed = true,
                Nome = "Tainara Vitoria",
                Sobrenome = " dos Santos",
                PhoneNumber = "14988060308",
                DataNascimento = new DateOnly(2001, 12, 19),
                TipoPerfil = Enums.TipoPerfil.Paciente,
                Foto = "/img/usuarios/paciente.png",
                PasswordHash = hashFixo,
                NormalizedEmail = "TAINARAVITSANTOS28@GMAIL.COM",
                NormalizedUserName = "TAINARAVITSANTOS28@GMAIL.COM",
                LockoutEnabled = true,
                SecurityStamp = "5b0faad3-6502-4325-94ee-33aab11905d7",
                ConcurrencyStamp = "7cb3541f-0085-4145-b6d7-3e9ae3a300a5",
                AccessFailedCount = 0 ,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            }
            ];


            builder.HasData(usuarios);
            #endregion

          
        }
    }
}
