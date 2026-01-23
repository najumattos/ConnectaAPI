using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations
{
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            List<Paciente> pacientes = [
                 new Paciente(){
                    UsuarioId = SeedDataConstants.USER_TAINARA_ID,
                    ContatoEmergencia = "14999009858",
                    QtdAcessos = 0, //contar automaticamente
                    HistoricoPaciente = "historico paciente",
                    PsicologoResponsavelId = SeedDataConstants.USER_ANA_JULIA_ID                    
                 }   
                 ];
            builder.HasData(pacientes);
        }
    }
}
