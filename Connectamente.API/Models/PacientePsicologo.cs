using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models
{
    [Table("PacientePsicologo")]
    public class PacientePsicologo
    {
        [Key]
        public int PacientePsicologoId { get; set; }

        [Display(Name = "Tipo de Paciente", Prompt = "Tipo de Paciente")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public TipoPaciente TipoPaciente { get; set; }

        public string PsicologoId { get; set; }
        [ForeignKey("PsicologoId")]
        public virtual Psicologo Psicologo { get; set; }
    }
}
