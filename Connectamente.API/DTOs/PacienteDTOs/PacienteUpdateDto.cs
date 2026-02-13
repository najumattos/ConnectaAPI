using Connectamente.API.Models.PacienteModel;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PacienteDTOs
{
    public class PacienteUpdateDto
    {
        public string ContatoEmergencia { get; set; }
        public string HistoricoPaciente { get; set; }
    }
}
