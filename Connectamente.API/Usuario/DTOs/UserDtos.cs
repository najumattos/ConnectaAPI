using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Usuario.DTOs;

public class UserDto
{
    [Required] public string Id { get; set; }
    [Required] public string Email { get; set; }
    [Required] public string Nome { get; set; }
    [Required] public string TipoPerfil { get; set; }
    [Required] public bool PerfilAtivo { get; set; }
    public string NomeCompleto { get; set; }
    public string Sobrenome { get; set; }
    public string DataNascimento { get; set; }
    public string Celular { get; set; }
    public string Foto { get; set; }

}