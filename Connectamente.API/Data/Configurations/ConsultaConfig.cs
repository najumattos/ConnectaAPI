using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations
{
    public class ConsultaConfig : IEntityTypeConfiguration<ConsultaModel>
    {
        public void Configure(EntityTypeBuilder<ConsultaModel> builder)
        {
            builder.HasOne(c => c.Prontuario)
       .WithMany(p => p.ConsultasVinculadas) 
       .HasForeignKey(c => c.ProntuarioId);

            builder.HasData(
                new ConsultaModel { 
                ConsultaId = SeedDataConstants.CONSULTA_ID,
                    AnotacoesConsulta = "Resumo sessao resumida",
                    DataHoraConsulta = new DateTime(2002, 4, 1),
                    DuracaoConsulta = TimeSpan.FromMinutes(50),
                    ProntuarioId = SeedDataConstants.PRONTUARIO_ID
                }
                );
        }
    }
}
