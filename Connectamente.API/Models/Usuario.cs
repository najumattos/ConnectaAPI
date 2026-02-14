using Connectamente.API.Enums;
using Connectamente.API.Validations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

    [Table("Usuario")]
    public class Usuario : IdentityUser
    {
    
    [Display(Name = "Nome do Usuário", Prompt = "Informe o nome"),
    Required(ErrorMessage = "Informe o nome do Usuario"),
    StringLength(150)]
    public string Nome { get; set; }

    [Display(Name = "Sobrenome do Usuário", Prompt = "Informe o sobrenome"),
    Required(ErrorMessage = "Informe o Sobrenome do Usuario"),
    StringLength(150)]
    public string Sobrenome { get; set; }

    [Display(Name = "Data De Nascimento", Prompt = "Informe a Data De Nascimento"),
    Required(ErrorMessage = "Informe a Data De Nascimento"),
    IdadeMinima(13, ErrorMessage = "Você precisa ter pelo menos 13 anos para se cadastrar."),
    LimitarDataFutura]
    public DateOnly DataNascimento { get; set; }

    [Display(Prompt = "Escolha uma Foto"), StringLength(300)]
    public string Foto { get; set; }

    public TipoPerfil TipoPerfil { get; set; } = TipoPerfil.Paciente;
    /*Cada dia que o usuario entra a contagem de acesso aumenta, esse dado é importante para contar "ofensivas" 
    Esse dado é relevante pra paciente apenas*/

    public int QtdAcessos { get; set; }
}
