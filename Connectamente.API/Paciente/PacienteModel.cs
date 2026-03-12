using Connectamente.API.Psicologo;
using Connectamente.API.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Paciente;

//Essa tabela de paciente serve para controle do psicologo.
[Table("PacienteModel")]
public class PacienteModel
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual UsuarioModel Usuario { get; set; }
                                                           
    [Required] public string ContatoEmergencia { get; set; }             

    [Display(Name = "Histórico do PacienteModel", Prompt = "Informações como se ja faz acompanhamento, uso de medicacao, diagnosticos previos, sono, alimentacao, uso de substancias, atividade fisica"),
    StringLength(1000), Required(ErrorMessage = "Campo obrigatório")]
    public string HistoricoPaciente { get; set; }
   
    public string PsicologoResponsavelId { get; set; }
    [ForeignKey("PsicologoResponsavelId")]
    public virtual PsicologoModel PsicologoResponsavel { get; set; }


}
