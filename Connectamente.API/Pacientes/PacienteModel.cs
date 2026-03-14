using Connectamente.API.RegistroConsulta;
using Connectamente.API.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Pacientes;
//renomear pacientesPsicologia para Pacientes. Um paciente pode ter varios prontuarios
//Essa tabela de paciente serve para controle do psicologo.
[Table("PacienteModel")]
public class PacienteModel
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual UsuarioModel Usuario { get; set; }         

    [Required(ErrorMessage = "O contato de emergência é obrigatório.")]
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O contato deve ter entre 10 e 11 dígitos numéricos.")]
    public int ContatoEmergencia { get; set; }

    [MaxLength(2000, ErrorMessage = "O histórico não pode exceder 2000 caracteres.")]
    public string HistoricoPaciente { get; set; }

    [MaxLength(1000)]
    public string UsoDeMedicamentos { get; set; }    

    //dados do paciente 
}
