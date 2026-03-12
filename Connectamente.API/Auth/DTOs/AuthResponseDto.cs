using Connectamente.API.Usuario.DTOs;

namespace Connectamente.API.Auth.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public UserDto User { get; set; } = null!;
}
