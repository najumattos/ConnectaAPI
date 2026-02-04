using System.ComponentModel;

namespace Connectamente.API.Enums
{
    public enum CondicaoTerapeutica
    {
        [Description("Condicões Terapêuticas")]
        CondicoesTerapeuticas,

        [Description("Estado de prostração profunda, perda de interesse e desânimo.")]
        Depressao,

        [Description("Preocupação excessiva, tensão e medo constante.")]
        Ansiedade,

        [Description("Processo de elaboração emocional após uma perda significativa.")]
        Luto,

        [Description("Acompanhamento psicológico durante o período de gravidez.")]
        Gestacao,

        [Description("Dificuldades de comunicação e convivência entre parceiros.")]
        ConflitoConjugal,

        [Description("Esgotamento profissional físico e mental extremo.")]
        Burnout,

        [Description("Dependência de substâncias químicas ou comportamentais.")]
        DependenciaQuimica,

        [Description("Dificuldade em aceitar a própria imagem ou baixa autoconfiança.")]
        BaixaAutoestima,

        [Description("Reações intensas após vivenciar eventos violentos ou traumáticos.")]
        EstressePosTraumatico,

        [Description("Anorexia, bulimia ou compulsão alimentar.")]
        TranstornoAlimentar,

        [Description("Dificuldade de concentração e inquietude motora.")]
        TDAH,

        [Description("Padrão de instabilidade emocional e nos relacionamentos.")]
        Borderline,

        [Description("Pensamentos intrusivos e comportamentos repetitivos.")]
        TOC,

        [Description("Medo irracional e paralisante de objetos ou situações.")]
        FobiaEspecifica,

        [Description("Acompanhamento para desenvolvimento de crianças no espectro.")]
        Autismo,

        [Description("Oscilações acentuadas entre euforia (mania) e depressão.")]
        Bipolaridade,

        [Description("Crises agudas de medo intenso e sintomas físicos.")]
        Pânico,

        [Description("Dificuldade em lidar com as mudanças da fase adolescente.")]
        ConflitoAdolescente,

        [Description("Questões relacionadas à memória e perda de autonomia na velhice.")]
        Geriatria,

        [Description("Dificuldades de aprendizado ou socialização no ambiente escolar.")]
        ProblemasEscolares
    }
}
