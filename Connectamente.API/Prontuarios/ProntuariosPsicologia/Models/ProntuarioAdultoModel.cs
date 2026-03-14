using Connectamente.API.Pacientes;
using Connectamente.API.Psicologo;
using Connectamente.API.RegistroConsulta;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Prontuarios.ProntuariosPsicologia.Models;

[Table("ProntuarioAdulto")]
public class ProntuarioAdultoModel
{
    public Guid IdProntuarioAdulto { get; set; }

    public string EstudanteResponsavelId { get; set; }
    [ForeignKey("EstudanteResponsavelId")]
    [Required(ErrorMessage = "O profissional é obrigatório.")]
    public virtual PsicologoModel EstudanteResponsavel { get; set; }

    public string PacientelId { get; set; }
    [ForeignKey("PacienteId")]
    [Required(ErrorMessage = "O paciente é obrigatório.")]
    public PacienteModel Paciente { get; set; }

    [Required]
    [Display(Name = "Data de Criação")]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    [Required]
    [Display(Name = "Última Atualização")]
    public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;

    public List<RegistroConsultaModel> Consultas { get; set; }
    //log datatime e quem atualizou

    //adicionar campos relacionados ao prontuario adulto
}
