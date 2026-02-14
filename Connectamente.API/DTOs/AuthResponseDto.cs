using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.DTOs;

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public UserDto User { get; set; } = null!;
}
