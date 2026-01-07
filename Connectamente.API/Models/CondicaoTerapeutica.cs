using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models
{
    [Table("CondicaoTerapeutica")]
    public class CondicaoTerapeutica
    {
        [Key]
        public int IdCondicaoTerapeutica { get; set; }

        [Display(Name = "Condição Terapêutica")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(150)]
        public string Nome { get; set; }

        [Display(Name = "Descrição", Prompt = "Descrição")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(1000)]
        public string Descricao { get; set; }
    }
}
