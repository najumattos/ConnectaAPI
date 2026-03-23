using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations;

public class ProntuarioConfig : IEntityTypeConfiguration<ProntuarioModel>
{
    public void Configure(EntityTypeBuilder<ProntuarioModel> builder)
    {
        // Relacionamento com Psicólogo
        builder.HasOne(p => p.PsicologoResponsavel)
               .WithMany(ps => ps.Prontuarios)
               .HasForeignKey(p => p.PsicologoResponsavelId)
               .OnDelete(DeleteBehavior.Restrict); // Evita deletar o psicólogo e apagar tudo por acidente

        // Relacionamento com Paciente
        builder.HasOne(p => p.Paciente)
               .WithMany() // Se o PacienteModel não tiver ICollection<ProntuarioModel>, deixe vazio
               .HasForeignKey(p => p.PacienteId)
               .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasData(
            new ProntuarioModel
            {
                ProntuarioId = SeedDataConstants.PRONTUARIO_ID,
                PsicologoResponsavelId = SeedDataConstants.USER_ESTUDANTE_ID,
                PacienteId = SeedDataConstants.USER_CLINICA_ID, 
                DataCriacao = new DateTime(2002, 4, 1),
                DataUltimaAtualizacao = new DateTime(2002, 4, 1),
                Queixas = "Paciente relata ansiedade e dificuldades para dormir.",
                TipoProntuario = Enums.TipoProntuarioEnum.Adulto
            });

    }
}