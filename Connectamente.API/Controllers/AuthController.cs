using Connectamente.API.DTOs;
using Connectamente.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Connectamente.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registra um novo usuário
    /// </summary>
    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromForm] RegisterCompleteDto registerCompleteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var result = await _authService.RegisterAsync(registerCompleteDto);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            // Aqui você captura a mensagem "Email já está em uso" que definiu no Service
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Erro interno no servidor." });
        }
    }

    /// <summary>
    /// Autentica um usuário
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(loginDto);
        if (result == null)
            return Unauthorized(new { message = "Email ou senha inválidos." });

        return Ok(result);
    }

    /// <summary>
    /// Obtém informações do usuário atual
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var user = await _authService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = "Usuário não encontrado." });

        return Ok(user);
    }

    /// <summary>
    /// Verifica se o token é válido
    /// </summary>
    [HttpGet("validate")]
    [Authorize]
    public ActionResult ValidateToken()
    {
        return Ok(new { message = "Token válido", isValid = true });
    }
}
