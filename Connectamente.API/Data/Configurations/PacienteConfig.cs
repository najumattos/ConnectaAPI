using Connectamente.API.Helpers;
using Connectamente.API.PacientesPsicologia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations;

public class PacienteConfig : IEntityTypeConfiguration<PacienteModel>
{
    public void Configure(EntityTypeBuilder<PacienteModel> builder)
    {
        List<PacienteModel> pacientes = [
             new PacienteModel(){
                UsuarioId = SeedDataConstants.USER_TAINARA_ID,
                ContatoEmergencia = "14999009858",
                HistoricoPaciente = "historico paciente",
                PsicologoResponsavelId = SeedDataConstants.USER_ANA_JULIA_ID,
             }                  
             ];
        builder.HasData(pacientes);
    }
}
