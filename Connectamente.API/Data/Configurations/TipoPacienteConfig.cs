using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Data.Configurations
{
    public class TipoPacienteConfig : IEntityTypeConfiguration<PacientePsicologo>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PacientePsicologo> builder)
        {
            builder.HasData(
            new PacientePsicologo
            {
                PacientePsicologoId = -1,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                TipoPaciente = Enums.TipoPaciente.Familiar
            },
            new PacientePsicologo
            {
                PacientePsicologoId = -2,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                TipoPaciente = Enums.TipoPaciente.Geriatrico
            }
        );
        }
    }
    
}
