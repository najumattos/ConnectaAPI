using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Models;

[Table("Consulta")]
public class ConsultaModel
{
    [Key]
    public string ConsultaId { get; set; }

    [Required] public DateTime DataHoraConsulta { get; set; }

    [Required] public TimeSpan DuracaoConsulta { get; set; }

    [StringLength(1000), Required(ErrorMessage = "Campo obrigatório")]
    public string AnotacoesConsulta { get; set; }

    public string ProntuarioId { get; set; }
    [ForeignKey("ProntuarioId")]
    public virtual ProntuarioModel Prontuario { get; set; }
    
}
