using System.ComponentModel;

namespace Connectamente.API.Enums
{
    public enum TipoPaciente
    {
        [Description("Tipos PacienteModel")]
        TiposPacientes = 0,

        [Description("PacienteModel na primeira infância (0 a 12 anos).")]
        Infantil = 1,

        [Description("Indivíduo na fase da adolescência (13 a 18 anos).")]
        Adolescente = 2,

        [Description("PacienteModel adulto (19 a 60 anos).")]
        Adulto = 3,

        [Description("PacienteModel idoso (acima de 60 anos).")]
        Geriatrico = 4,

        [Description("Atendimento realizado no domicílio do paciente.")]
        HomeCare = 5,

        [Description("Atendimento focado na dinâmica do grupo familiar.")]
        Familiar = 6,

        [Description("Casal em processo terapêutico conjunto.")]
        Casal = 7,

        [Description("PacienteModel encaminhado por medidas socioeducativas ou judiciais.")]
        Juridico = 8,

        [Description("Atendimento para funcionários de empresas conveniadas.")]
        Corporativo = 9
    }
}
