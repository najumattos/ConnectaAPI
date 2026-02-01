using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.UsersDTOs;

public class UserDto
{
    [Required] public string Id { get; set; }
    [Required] public string Email { get; set; }
    public string NomeCompleto { get; set; }
    [Required] public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string DataNascimento { get; set; }
    public string Celular { get; set; }
    public string Foto { get; set; }
    [Required] public string TipoPerfil { get; set; }
}