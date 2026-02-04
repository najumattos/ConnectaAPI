using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PacienteDTOs
{
    public class PacienteDto
    {
        [Required] public string IdPaciente { get; set; }
        [Required] public string ContatoEmergencia { get; set; }
        [Required] public string HistoricoPaciente { get; set; }
        [Required] public string PsicologoResponsavel { get; set; }

    }
}
