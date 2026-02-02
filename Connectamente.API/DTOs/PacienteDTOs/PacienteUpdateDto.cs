using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PacienteDTOs
{
    public class PacienteUpdateDto
    {
        [Required] public string ContatoEmergencia { get; set; }
        [Required] public string HistoricoPaciente { get; set; }
    }
}
