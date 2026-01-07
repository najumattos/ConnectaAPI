using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models
{
    [Table("TipoPaciente")]
    public class TipoPaciente
    {
        [Key]
        public int IdTipoPaciente { get; set; }

        [Display(Name = "Com quais tipos de pacientes você trabalha?")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(150)]
        public string Nome { get; set; }

        [Display(Name = "Descrição", Prompt = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(1000)]
        public string Descricao { get; set; }
    }
}
