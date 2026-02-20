using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Connectamente.API.Data.Configurations;

public class PsicoConfig : IEntityTypeConfiguration<Psicologo>
{
    public void Configure(EntityTypeBuilder<Psicologo> builder)
    {
        builder.HasData(
 new Psicologo
 {
     UsuarioId = SeedDataConstants.USER_ANA_JULIA_ID,
     CRP = "12345",
     Descricao = "Psicóloga dedicada a ajudar pacientes a superar desafios emocionais e alcançar bem-estar mental.",
     ModalidadeDeAtendimento = Enums.ModalidadeAtendimento.Presencial,
     AbordagensTerapeuticas = new List<AbordagemTerapeutica>
    {
        AbordagemTerapeutica.TCC,
        AbordagemTerapeutica.Psicanalise
    },

     CondicoesTerapeuticas = new List<CondicaoTerapeutica>
    {
        CondicaoTerapeutica.Ansiedade,
        CondicaoTerapeutica.Depressao
    },

     TiposPacientes = new List<TipoPaciente>
    {
        TipoPaciente.Adulto,
        TipoPaciente.Adolescente
    }
 }
        );
    }
}
