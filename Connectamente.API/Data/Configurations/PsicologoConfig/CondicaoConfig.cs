using Connectamente.API.Helpers;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations.PsicologoConfig;

public class CondicaoConfig : IEntityTypeConfiguration<CondicoesTratadas>
{
    public void Configure(EntityTypeBuilder<CondicoesTratadas> builder)
    {
        builder.HasData(
       new CondicoesTratadas
       {
           CondicaoPsicologoId = -1,
           PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
           CondicaoTerapeutica = Enums.CondicaoTerapeutica.Gestacao
       },
       new CondicoesTratadas
       {
           CondicaoPsicologoId = -2,
           PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
           CondicaoTerapeutica = Enums.CondicaoTerapeutica.FobiaEspecifica
       }
   );
    }
}
