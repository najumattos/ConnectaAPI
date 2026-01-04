using Connectamente.API.DTOs;

namespace Connectamente.API.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserDto user);
}
