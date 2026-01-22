using Connectamente.API.Enums;
using Connectamente.API.Validations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

    [Table("Usuario")]
    public class Usuario : IdentityUser
    {
    //o gemini me explicou que essa classe nao pode ter um campo de ID por causa do Identity mas no projeto interdisciplinar ta assim e que o identity tem um campo Id string

    //nome e sobrenome separados para que o site possar referir-se ao usuario apenas pelo primeiro nome
    [Display(Name = "Nome do Usuário", Prompt = "Informe o nome")]
    [Required(ErrorMessage = "Informe o nome do Usuario")]
    [StringLength(150)]
    public string Nome { get; set; }

    [Display(Name = "Sobrenome do Usuário", Prompt = "Informe o sobrenome")]
    [Required(ErrorMessage = "Informe o Sobrenome do Usuario")]
    [StringLength(150)]
    public string Sobrenome { get; set; }

    [Display(Name = "Data De Nascimento", Prompt = "Informe a Data De Nascimento")]
    [Required(ErrorMessage = "Informe a Data De Nascimento")]
    [IdadeMinima(13, ErrorMessage = "Você precisa ter pelo menos 13 anos para se cadastrar.")]
    [LimitarDataFutura]
    public DateOnly DataNascimento { get; set; }

    [Display(Prompt = "Escolha uma Foto")]    
    [StringLength(300)]
    public string Foto { get; set; }

    //Cada dia que o usuario entra a contagem de acesso aumenta, esse dado é importante para contar "ofensivas" 
    public int QtdAcessos { get; set; }

    public TipoPerfil TipoPerfil { get; set; } = TipoPerfil.Paciente;

    //todo psicologo é um usuario mas se o tipo for paciente, ele pode ter um psicologo responsavel
    //pode ser nulo pois nem todo usuario precisa ter um psicologo atribuido
    public string? PsicologoResponsavelId { get; set; }
    [ForeignKey("PsicologoResponsavelId")]
    public virtual Psicologo? PsicologoResponsavel { get; set; }

    // email, telefone e senha ja vem da IdentityUser
}
