using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models.RPD;

[Table("RegistroPensamento")]
public class RegistroPensamento
{
    [Key]
    public int RegistroId { get; set; }
    public DateTime DataHora { get; set; }

    [StringLength(255)]
    public string CaminhoArquivoRegistro { get; set; }

    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; }

}
