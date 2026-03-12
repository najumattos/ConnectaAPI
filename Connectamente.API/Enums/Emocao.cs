using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Enums;

public enum Emocao
{
    [Display(Name = "Emoção")]
    Emocao = 1,

    [Display(Name = "Tristeza")]
    Tristeza = 2,

    [Display(Name = "Raiva")]
    Raiva = 3,

    [Display(Name = "Medo")]
    Medo = 4,

    [Display(Name = "Alegria")]
    Alegria = 5,

    [Display(Name = "Nojo")]
    Nojo = 6,

    [Display(Name = "Ansiedade")]
    Ansiedade = 7,

    [Display(Name = "Culpa")]
    Culpa = 8
}
