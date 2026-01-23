using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations
{
    public class AbordagemConfig : IEntityTypeConfiguration<AbordagemPsicologo>
    {
        public void Configure(EntityTypeBuilder<AbordagemPsicologo> builder)
        {
            builder.HasData(
            new AbordagemPsicologo
            {
                AbordagemPsicologoId = -1,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                AbordagemTerapeutica = Enums.AbordagemTerapeutica.TCC
            },
            new AbordagemPsicologo
            {
                AbordagemPsicologoId = -2,
                PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
                AbordagemTerapeutica = Enums.AbordagemTerapeutica.Psicanalise
            }
        );
        }
    }
}
