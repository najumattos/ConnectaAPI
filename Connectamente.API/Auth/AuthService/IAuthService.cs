using Connectamente.API.Auth.DTOs;
using Connectamente.API.Usuario.DTOs;

namespace Connectamente.API.Auth.AuthService;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> GetUserByIdAsync(string userId);
}
