using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

[Table("AbordagemTerapeutica")]
public class AbordagemTerapeutica
{
    [Key]
    public int IdAbordagemTerapeutica { get; set; }

    [Display(Name = "Abordagem Terapêutica", Prompt = "Abordagem Terapêutica")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [StringLength(150)]
    public string Nome { get; set; }

    [Display(Name = "Descrição", Prompt = "Descrição")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [StringLength(1000)]
    public string Descricao { get; set; }
}
