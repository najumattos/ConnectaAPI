using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.JwtService;
using Connectamente.API.Services.UsuarioService;
using Microsoft.AspNetCore.Identity;

namespace Connectamente.API.Services.AuthService;

public class AuthService(
    UserManager<Usuario> userManager,
    SignInManager<Usuario> signInManager,
    IJwtService jwtService,
    IFileService fileService,
    IUsuarioService usuarioService
    ) : IAuthService
{    

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário e/ou Senha Inválidos.");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Usuário e/ou Senha Inválidos.");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            Celular = user.PhoneNumber,
            NomeCompleto = $"{user.Nome} {user.Sobrenome}",
            DataNascimento = user.DataNascimento.ToString(),
            TipoPerfil = user.TipoPerfil.ToString(),
            Foto = !string.IsNullOrEmpty(user.Foto) ? fileService.GetFileUrl(user.Foto) : null
        };
        var token = jwtService.GenerateToken(userDto);

        return new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60),
            User = userDto
        };
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            Celular = user.PhoneNumber,
            NomeCompleto = $"{user.Nome} {user.Sobrenome}",
            DataNascimento = user.DataNascimento.ToString(),
            TipoPerfil = user.TipoPerfil.ToString(),
            Foto = !string.IsNullOrEmpty(user.Foto) ? fileService.GetFileUrl(user.Foto) : null
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("Email já está em uso.");
        }
            
        // Salvar a foto se existir
        string fotoPath = null;
        if (registerDto.Foto != null)
        {
            fotoPath = await fileService.SaveFileAsync(registerDto.Foto, "img/usuarios");
        }
        var user = new Usuario
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            Nome = registerDto.Nome,
            Sobrenome = registerDto.Sobrenome,
            DataNascimento = registerDto.DataNascimento,
            PhoneNumber = registerDto.Celular,
            Foto = fotoPath,
            TipoPerfil = registerDto.TipoPerfil
        };



        var usuarioDto = usuarioService.MapearParaResposta(user);

        var result = await userManager.CreateAsync(user, registerDto.Senha);
        if (!result.Succeeded)
        {
            if (fotoPath != null)
                await fileService.DeleteFileAsync(fotoPath);

            var errors = string.Join(", ", result.Errors.Select(e => TranslateIdentityErrors.TranslateErrorMessage(e.Code)));
            throw new ArgumentException($"Falha ao criar usuário: {errors}");
        }

        await userManager.AddToRoleAsync(user, "Paciente");

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            Celular = user.PhoneNumber,
            NomeCompleto = $"{user.Nome} {user.Sobrenome}",
            DataNascimento = user.DataNascimento.ToString(),
            TipoPerfil = user.TipoPerfil.ToString(),
            Foto = fotoPath != null ? fileService.GetFileUrl(fotoPath) : null
        };
        var token = jwtService.GenerateToken(userDto);

        return new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60),
            User = userDto
        };
    }
}
