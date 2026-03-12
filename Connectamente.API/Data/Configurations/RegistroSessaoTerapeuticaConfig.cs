using Connectamente.API.Helpers;
using Connectamente.API.RegistroConsulta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations
{
    public class RegistroSessaoTerapeuticaConfig : IEntityTypeConfiguration<RegistroConsultaModel>
    {
        public void Configure(EntityTypeBuilder<RegistroConsultaModel> builder)
        {
            builder.HasData(
                new RegistroConsultaModel { 
                RegistroSessaoId = -1,
                ResumoSessao = "Resumo sessao",
                DataHoraSessao = new DateTime(2002, 4, 1),
                DuracaoSessao = TimeSpan.FromMinutes(50),
                PacienteId = SeedDataConstants.USER_TAINARA_ID,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID
                }
                );
        }
    }
}
