using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations;

public class PsicologoConfig : IEntityTypeConfiguration<PsicologoModel>
{
    public void Configure(EntityTypeBuilder<PsicologoModel> builder)
    {
        // 1. Define que o UsuarioId é a Chave Primária
        builder.HasKey(p => p.UsuarioId);

        // 2. Configura o relacionamento 1:1 com o Usuario
        builder.HasOne(p => p.Usuario)
               .WithOne() // Se o UsuarioModel não tiver uma propriedade 'Psicologo', deixe vazio
               .HasForeignKey<PsicologoModel>(p => p.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
 new PsicologoModel
 {
     UsuarioId = SeedDataConstants.USER_ESTUDANTE_ID,
     CRP = "12345",
     Descricao = "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental."
 }
        //prontuarios e consultas
        );
    }
}
