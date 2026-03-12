using Connectamente.API.Psicologo;

namespace Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs
{
    public class ProntuarioPacienteDto
    {
        public string IdPaciente { get; set; }
        public string ContatoEmergencia { get; set; }
        public string HistoricoPaciente { get; set; }
        public string PsicologoResponsavel { get; set; }
    }
}
