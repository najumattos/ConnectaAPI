using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.Services.JwtService;

public interface IJwtService
{
    string GenerateToken(UserDto user);
}
