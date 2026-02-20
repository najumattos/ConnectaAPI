using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models.PsicologoModel;

namespace Connectamente.API.Models;

[Table("RegistroSessao")]
public class RegistroSessao
{
    [Key]
    public int RegistroSessaoId { get; set; }

    [Required] public DateTime DataHoraSessao { get; set; }

    [Required] public TimeSpan DuracaoSessao { get; set; }

    [Display(Name = "Resumo da Sessão", Prompt = "Adicione aqui suas anotações sobre a sessão"),
    StringLength(1000), Required(ErrorMessage = "Campo obrigatório")]
    public string ResumoSessao { get; set; }

    public string PacienteId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual Paciente Paciente { get; set; }

    public string PsicologoId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual Psicologo Psicologo { get; set; }
}
