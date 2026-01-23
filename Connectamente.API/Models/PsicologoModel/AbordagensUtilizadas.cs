using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models.PsicologoModel;

[Table("AbordagemPsicologo")]
public class AbordagensUtilizadas
{
    [Key]
    public int AbordagemPsicologoId { get; set; }

    [Display(Name = "Abordagem Terapêutica", Prompt = "Abordagem Terapêutica")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    public AbordagemTerapeutica AbordagemTerapeutica { get; set; }

    public string PsicologoId { get; set; }// Deve ser string porque UsuarioId no Identity é string
    [ForeignKey("PsicologoId")]
    public virtual Psicologo Psicologo { get; set; }

}
