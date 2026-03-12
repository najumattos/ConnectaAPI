using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Auth.DTOs;

public class LoginDto
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Senha { get; set; }
}
