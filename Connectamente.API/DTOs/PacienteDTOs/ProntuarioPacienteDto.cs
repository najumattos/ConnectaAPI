using Connectamente.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PacienteDTOs
{
    public class ProntuarioPacienteDto
    {
        [Required] public string IdPaciente { get; set; }
        [Required] public string ContatoEmergencia { get; set; }
        [Required] public string HistoricoPaciente { get; set; }
        public string PsicologoResponsavel { get; set; }
        public List<RegistroSessao> SessoesRegistradas { get; set; }
    }
}
