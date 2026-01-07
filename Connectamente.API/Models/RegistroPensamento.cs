using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

[Table("RegistroPensamento")]
public class RegistroPensamento
{
    [Key]
    public int IdRegistro { get; set; }
    public DateTime DataHora { get; set; }

    [StringLength(255)]
    public string CaminhoArquivoRegistro { get; set; }

    public string UsuarioId { get; set; } 
    [ForeignKey("UsuarioId")] 
    public virtual Usuario Usuario { get; set; }

    //nao sei o quanto isso aqui ta certo
   /* public ICollection<EmocaoRegistro> EmocoesIniciais { get; set; }
    public ICollection<EmocaoRegistro> EmocoesFinais { get; set; }*/
}
