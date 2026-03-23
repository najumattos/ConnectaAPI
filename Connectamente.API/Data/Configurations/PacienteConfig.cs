using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations;

public class PacienteConfig : IEntityTypeConfiguration<PacienteModel>
{
    public void Configure(EntityTypeBuilder<PacienteModel> builder)
    {
        // 1. Define que o UsuarioId é a Chave Primária
        builder.HasKey(p => p.UsuarioId);

        // 2. Configura o relacionamento 1:1 com o Usuario
        builder.HasOne(p => p.Usuario)
               .WithOne() // Se o UsuarioModel não tiver uma propriedade 'Psicologo', deixe vazio
               .HasForeignKey<PacienteModel>(p => p.UsuarioId)
               .OnDelete(DeleteBehavior.Cascade);
        List<PacienteModel> pacientes = [
             new PacienteModel(){
                UsuarioId = SeedDataConstants.USER_CLINICA_ID,
                ContatoEmergencia = "14999009858",
                HistoricoPaciente = "historico paciente"
             }                  
             ];
        builder.HasData(pacientes);
    }
}
