using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models
{
    [Table("CondicaoPsicologo")]
    public class CondicaoPsicologo
    {
        [Key]
        public int CondicaoPsicologoId { get; set; }

        [Display(Name = "Condição Terapêutica", Prompt = "Condição Terapêutica")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public CondicaoTerapeutica CondicaoTerapeutica { get; set; }

        public string PsicologoId { get; set; }
        [ForeignKey("PsicologoId")]
        public virtual Psicologo Psicologo { get; set; }
    }
}
