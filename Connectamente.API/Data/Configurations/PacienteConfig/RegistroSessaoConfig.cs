using Connectamente.API.Helpers;
using Connectamente.API.Models.PacienteModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations.PacienteConfig
{
    public class RegistroSessaoConfig : IEntityTypeConfiguration<RegistroSessao>
    {
        public void Configure(EntityTypeBuilder<RegistroSessao> builder)
        {
            builder.HasData(
       new RegistroSessao
       {
             RegistroSessaoId = -1,
             DataHoraSessao = new DateTime(2026, 4, 1, 16,30,00),
             DuracaoSessao = new TimeSpan(50),
             ResumoSessao = "Resumo Sessao 1",
             PacienteId = SeedDataConstants.USER_TAINARA_ID


       },
        new RegistroSessao
        {
            RegistroSessaoId = -2,
            DataHoraSessao = new DateTime(2026, 5, 1, 16, 30, 00),
            DuracaoSessao = new TimeSpan(50),
            ResumoSessao = "Resumo Sessao 2",
            PacienteId = SeedDataConstants.USER_TAINARA_ID
        }
       );
       }
    }
}
