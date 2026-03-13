using Connectamente.API.PacientesPsicologia;
using Connectamente.API.Psicologo;
using Connectamente.API.RegistroConsulta;
using Connectamente.API.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Prontuarios.ProntuariosPsicologia.Models;

[Table("ProntuarioInfantil")]
public class ProntuarioInfantilModel
{
    [Key]
    public Guid IdProntuarioInfantil { get; set; }

    public string EstudanteResponsavelId { get; set; }
    [ForeignKey("EstudanteResponsavelId")]
    [Required(ErrorMessage = "O profissional é obrigatório.")]
    public virtual PsicologoModel EstudanteResponsavel { get; set; }

    public string PacienteInfantilId { get; set; }
    [ForeignKey("PacienteInfantilId")]
    [Required(ErrorMessage = "O paciente é obrigatório.")]
    public PacienteModel PacienteInfantil { get; set; }

    public string ResponsavelCriancaId { get; set; }
    [ForeignKey("UsuarioId")]
    [Required(ErrorMessage = "O responsavel é obrigatório.")]
    public virtual UsuarioModel ResponsavelCrianca { get; set; }

    [Required]
    [Display(Name = "Data de Criação")]
    public DateTime DataCriacao { get; set; } = DateTime.Now;    

    [Required]
    [Display(Name = "Última Atualização")]
    public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;
    //log datatime e quem atualizou

    //adicionar campos relacionados ao prontuario infantil

}
