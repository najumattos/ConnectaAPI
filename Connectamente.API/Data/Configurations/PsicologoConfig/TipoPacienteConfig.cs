using Connectamente.API.Helpers;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Data.Configurations.PsicologoConfig;

public class TipoPacienteConfig : IEntityTypeConfiguration<TiposPacienteTratados>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TiposPacienteTratados> builder)
    {
        builder.HasData(
        new TiposPacienteTratados
        {
            PacientePsicologoId = -1,
            PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
            TipoPaciente = Enums.TipoPaciente.Familiar
        },
        new TiposPacienteTratados
        {
            PacientePsicologoId = -2,
            PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
            TipoPaciente = Enums.TipoPaciente.Geriatrico
        }
    );
    }
}
