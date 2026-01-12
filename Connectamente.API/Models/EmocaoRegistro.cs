using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

[Table("EmocaoRegistro")]
public class EmocaoRegistro

{
    [Key]
    public int EmocaoRegistroId { get; set; }

    [Display(Name = "Emoção", Prompt = "Emoção")]
    public Emocao Emocao { get; set; }

    [Display(Name = "Intensidade Inicial")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [Range(0, 100, ErrorMessage = "A intensidade deve ser entre 0 e 100")]
    [RegularExpression(@"^(0|10|20|30|40|50|60|70|80|90|100)$", ErrorMessage = "A intensidade deve ser múltipla de 10 (ex: 10, 20...)")]
    public int IntensidadeInicial { get; set; }

    [Display(Name = "Intensidade Final")]
    [Required(ErrorMessage = "Campo Obrigatório")]
    [Range(0, 100, ErrorMessage = "A intensidade deve ser entre 0 e 100")]
    [RegularExpression(@"^(0|10|20|30|40|50|60|70|80|90|100)$", ErrorMessage = "A intensidade deve ser múltipla de 10 (ex: 10, 20...)")]
    public int IntensidadeFinal { get; set; }

    public int RegistroPensamentoId { get; set; }
    [ForeignKey("RegistroPensamentoId")]
    public virtual RegistroPensamento RegistroPensamento { get; set; }
}
