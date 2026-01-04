using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Enums;

public enum Emocao
{
    [Display(Name = "Tristeza")]
    Tristeza = 1,

    [Display(Name = "Raiva")]
    Raiva = 2,

    [Display(Name = "Medo")]
    Medo = 3,

    [Display(Name = "Alegria")]
    Alegria = 4,

    [Display(Name = "Nojo")]
    Nojo = 5,

    [Display(Name = "Ansiedade")]
    Ansiedade = 6,

    [Display(Name = "Culpa")]
    Culpa = 7
}
