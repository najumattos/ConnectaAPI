using Connectamente.API.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

[Table("Prontuario")]
public class ProntuarioModel
{
    [Key] public string ProntuarioId { get; set; }

    public string PsicologoResponsavelId { get; set; }
    [ForeignKey("PsicologoResponsavelId")]
    [Required(ErrorMessage = "O psicologo é obrigatório.")]
    public virtual PsicologoModel PsicologoResponsavel { get; set; }


    public string PacienteId { get; set; }
    [ForeignKey("PacienteId")]
    [Required(ErrorMessage = "O paciente é obrigatório.")]
    public virtual PacienteModel Paciente { get; set; }

    [Required]
    [Display(Name = "Data de Criação")]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    [Required]
    [Display(Name = "Última Atualização")]
    public DateTime DataUltimaAtualizacao { get; set; } = DateTime.Now;

    public string Queixas { get; set; }
    public TipoProntuarioEnum TipoProntuario { get; set; }
    public IEnumerable<ConsultaModel> ConsultasVinculadas { get; set; }
}

