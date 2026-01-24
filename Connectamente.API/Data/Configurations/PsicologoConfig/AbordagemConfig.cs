using Connectamente.API.Helpers;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations.PsicologoConfig;

public class AbordagemConfig : IEntityTypeConfiguration<AbordagensUtilizadas>
{
    public void Configure(EntityTypeBuilder<AbordagensUtilizadas> builder)
    {
        builder.HasData(
        new AbordagensUtilizadas
        {
            AbordagemPsicologoId = -1,
            PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
            AbordagemTerapeutica = Enums.AbordagemTerapeutica.TCC
        },
        new AbordagensUtilizadas
        {
            AbordagemPsicologoId = -2,
            PsicologoId = SeedDataConstants.USER_ANA_JULIA_ID,
            AbordagemTerapeutica = Enums.AbordagemTerapeutica.Psicanalise
        }
    );
    }
}
