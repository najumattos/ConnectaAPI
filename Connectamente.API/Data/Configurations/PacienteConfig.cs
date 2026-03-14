using Connectamente.API.Helpers;
using Connectamente.API.Pacientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations;

public class PacienteConfig : IEntityTypeConfiguration<PacienteModel>
{
    public void Configure(EntityTypeBuilder<PacienteModel> builder)
    {
        long contatoEmergencia = 14999009858;
        List<PacienteModel> pacientes = [
             new PacienteModel(){
                UsuarioId = SeedDataConstants.USER_TAINARA_ID,
                ContatoEmergencia = contatoEmergencia,
                HistoricoPaciente = "historico paciente",
                 }                  
             ];
        builder.HasData(pacientes);
    }
}
