using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

//Essa tabela de paciente serve para controle do psicologo.
[Table("Paciente")]
public class PacienteModel
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual UsuarioModel Usuario { get; set; }

    [Required] public string ContatoEmergencia { get; set; }

    [Display(Name = "Histórico do Paciente", Prompt = "Informações como se ja faz acompanhamento, uso de medicacao, diagnosticos previos, sono, alimentacao, uso de substancias, atividade fisica"),
    StringLength(1000), Required(ErrorMessage = "Campo obrigatório")]
    public string HistoricoPaciente { get; set; }    

}
