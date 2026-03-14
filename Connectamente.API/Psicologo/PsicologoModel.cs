using Connectamente.API.Enums;
using Connectamente.API.Pacientes;
using Connectamente.API.Prontuarios.ProntuariosPsicologia;
using Connectamente.API.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Psicologo;

[Table("PsicologoModel")]
public class PsicologoModel
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual UsuarioModel Usuario { get; set; }

    //nº de registro como psicologo
    [Required(ErrorMessage = "O CRP é obrigatório")]
    public string CRP { get; set; }

    [Display(Name = "Sobre o PsicologoModel", Prompt = "Descreva você e seu trabalho"),
    StringLength(1000), Required(ErrorMessage = "Campo obrigatório")]
    public string Descricao { get; set; }

    [Display(Name = "Modalidades de Atendimento", Prompt = "Insira as modalidades de atendiemnto"),
    Required(ErrorMessage = "Campo obrigatório")]
    public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }

    [Display(Name = "Tipo paciente que você atende"),
    Required(ErrorMessage = "Campo obrigatório")]
    public List<TipoProntuarioEnum> TiposPacientes { get; set; }
    
    [Display(Name = "Tipo de condições que você trata"),
    Required(ErrorMessage = "Campo obrigatório")]
    public List<CondicaoTerapeutica> CondicoesTerapeuticas { get; set; }

    [Display(Name = "Abordagens Terapeuticas", Prompt = "Insira suas Abordagens Terapeuticas"),
    Required(ErrorMessage = "Campo obrigatório")]
    public List<AbordagemTerapeutica> AbordagensTerapeuticas { get; set; }
    public ICollection<PacienteModel> PacientesVinculados { get; set; }
}
