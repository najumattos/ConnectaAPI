using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PacienteDTOs
{
    public class PerfilPaciente
    {
        [Required] public string IdPaciente { get; set; }

    }
}
