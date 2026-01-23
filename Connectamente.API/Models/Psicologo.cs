using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

[Table("Psicologo")]
public class Psicologo
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; }

    //nº de registro como psicologo
    [Required(ErrorMessage = "O CRP é obrigatório")]
    public string CRP { get; set; }

    [Display(Name = "Sobre o Psicologo", Prompt = "Descreva você e seu trabalho")]
    [StringLength(1000)]
    [Required(ErrorMessage = "Campo obrigatório")]
    public string Descricao { get; set; }

    [Display(Name = "Modalidades de Atendimento", Prompt = "Insira as modalidades de atendiemnto")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }

    [Display(Name = "Tipo paciente que você atende")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public virtual ICollection<PacientePsicologo> TiposPacientes { get; set; } = new List<PacientePsicologo>();

    [Display(Name = "Tipo de condições que você trata")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public virtual ICollection<CondicaoPsicologo> CondicoesTerapeuticas { get; set; } = new List<CondicaoPsicologo>();

    [Display(Name = "Abordagens Terapeuticas", Prompt = "Insira suas Abordagens Terapeuticas")]
    [Required(ErrorMessage = "Campo obrigatório")]
    public virtual ICollection<AbordagemPsicologo> AbordagensTerapeuticas { get; set; } = new List<AbordagemPsicologo>();
}
