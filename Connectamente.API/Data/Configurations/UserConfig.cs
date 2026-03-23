using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UsuarioModel> builder)
        {

            string hashFixo = "AQAAAAIAAYagAAAAEJ9FzXF/zP/9q8m6sF3jKx5T6P6lB6m1z2x3c4v5b6n7m8==";
            #region Populate Usuário
            List<UsuarioModel> usuarios = [
                   new UsuarioModel(){
                Id = SeedDataConstants.USER_ESTUDANTE_ID,
                UserName = "estudante@psico.com",
                Email = "estudante@psico.com",
                EmailConfirmed = true,
                NormalizedEmail = "ESTUDANTE@PSICO.COM",
                Nome = "Ana Julia",
                Sobrenome = "Estudante Psicologia",
                PhoneNumber = "5514920044824",
                DataNascimento = new DateOnly(2002, 4, 1),
                Foto = "/img/usuarios/estudante.png",
                TipoPerfil = TipoPerfilEnum.Psicologo,
                NormalizedUserName = "ESTUDANTE@PSICO.COM",
                LockoutEnabled = true,
                PasswordHash = hashFixo,
                SecurityStamp = "15cfe30f-1dac-404e-85e6-02159dbed489",
                ConcurrencyStamp = "5458aee0-71ca-4f08-88e8-0f03d18d6960",
                AccessFailedCount = 0 ,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            },
              new UsuarioModel(){
                Id = SeedDataConstants.USER_ADMINISTRADOR_ID,
                UserName = "admin@psico.com",
                Email = "admin@psico.com",
                EmailConfirmed = true,
                Nome = "Tainara",
                Sobrenome = "Administrador Psicologia",
                PhoneNumber = "5514988060308",
                DataNascimento = new DateOnly(2001, 12, 19),
                TipoPerfil = TipoPerfilEnum.Coordenador,
                Foto = "/img/usuarios/admin.png",
                PasswordHash = hashFixo,
                NormalizedEmail = "ADMIN@PSICO.COM",
                NormalizedUserName = "ADMIN@PSICO.COM",
                LockoutEnabled = true,
                SecurityStamp = "5b0faad3-6502-4325-94ee-33aab11905d7",
                ConcurrencyStamp = "7cb3541f-0085-4145-b6d7-3e9ae3a300a5",
                AccessFailedCount = 0 ,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false
            },
              new UsuarioModel(){
                Id = SeedDataConstants.USER_CLINICA_ID,
                UserName = "clinica@psico.com",
                Email = "clinica@psico.com",
                EmailConfirmed = true,
                Nome = "Gallo",
                Sobrenome = "Clinica Psicologia",
                PhoneNumber = "5514991044050",
                DataNascimento = new DateOnly(2001, 07, 13),
                TipoPerfil = TipoPerfilEnum.Clinica,
                Foto = "/img/usuarios/clinica.png",
                PasswordHash = hashFixo,
                NormalizedEmail = "CLINICA@PSICO.COM",
                NormalizedUserName = "CLINICA@PSICO.COM",
                LockoutEnabled = true,
                SecurityStamp = "ddaeca55-d4a8-40bd-8dce-21df6b31d700",     //  guidgenerator
                ConcurrencyStamp = "0565c7de-832d-4003-bdd3-5386ff2a214b",
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
