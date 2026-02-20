using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations
{
    public class RegistroSessaoTerapeuticaConfig : IEntityTypeConfiguration<RegistroSessao>
    {
        public void Configure(EntityTypeBuilder<RegistroSessao> builder)
        {
            builder.HasData(
                new RegistroSessao { 
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
