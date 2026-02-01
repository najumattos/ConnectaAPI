using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.Services.AuthService;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<UserDto> GetUserByIdAsync(string userId);
}
