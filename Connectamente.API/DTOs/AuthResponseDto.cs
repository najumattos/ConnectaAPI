using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Enums;

namespace Connectamente.API.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public UserDto User { get; set; } = null!;
    public TipoPerfilEnum? TipoPerfil { get; set; }
}
