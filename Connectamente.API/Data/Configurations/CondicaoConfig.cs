using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations
{
    public class CondicaoConfig : IEntityTypeConfiguration<CondicaoPsicologo>
    {
        public void Configure(EntityTypeBuilder<CondicaoPsicologo> builder)
        {
            builder.HasData(
           new CondicaoPsicologo
           {
               CondicaoPsicologoId = -1,
               PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
               CondicaoTerapeutica = Enums.CondicaoTerapeutica.Gestacao
           },
           new CondicaoPsicologo
           {
               CondicaoPsicologoId = -2,
               PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
               CondicaoTerapeutica = Enums.CondicaoTerapeutica.FobiaEspecifica
           }
       );
        }
    }
}
