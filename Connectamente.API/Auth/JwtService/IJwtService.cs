using Connectamente.API.Usuario.DTOs;

namespace Connectamente.API.Auth.JwtService;

public interface IJwtService
{
    string GenerateToken(UserDto user);
}
