using AutoMapper;
using Connectamente.API.Data;
using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.JwtService;
using Connectamente.API.Services.UsuarioService;
using Connectamente.API.Usuario;
using Microsoft.AspNetCore.Identity;

namespace Connectamente.API.Services.AuthService;

public class AuthService(
    UserManager<UsuarioModel> userManager,
    SignInManager<UsuarioModel> signInManager,
    IJwtService jwtService,
    IFileService fileService,
    IUsuarioService usuarioService,
    AppDbContext context,
    IMapper mapper
    ) : IAuthService
{

    /// <summary>
    /// Login OKAY
    /// </summary>
    public async Task<Result<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await VerificarLogin(loginDto);
        var userDto = mapper.Map<UserDto>(user);
        var token = jwtService.GenerateToken(userDto);
        await context.SaveChangesAsync();
        var authResponseDto = mapper.Map<AuthResponseDto>(userDto);
        authResponseDto.Token = token;
        return Result<AuthResponseDto>.Success(authResponseDto);
    }

    /// <summary>
    /// Buscar Usuario Por Id OKAY
    /// </summary>
    public async Task<Result<UserDto>> GetUserByIdAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            Result<UserDto>.Failure("Usuario não encontrado");
        }

        var userDto = mapper.Map<UserDto>(user);

        return Result<UserDto>.Success(userDto);
    }

    /// <summary>
    /// Registrar Usuario REVISAR
    /// </summary>
    public async Task<Result<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {

        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
        {
            Result<AuthResponseDto>.Failure("Email não encontrado");
        }

        // Salvar a foto se existir
        string fotoPath = null;
        if (registerDto.Foto != null)
        {
            fotoPath = await fileService.SaveFileAsync(registerDto.Foto, "img/usuarios");
        }
        var user = mapper.Map<UsuarioModel>(registerDto);
        user.Foto = fotoPath;
        var result = await userManager.CreateAsync(user, registerDto.Senha);
        if (!result.Succeeded)
        {
            if (fotoPath != null)
                await fileService.DeleteFileAsync(fotoPath);

            var errors = string.Join(", ", result.Errors.Select(e => TranslateIdentityErrors.TranslateErrorMessage(e.Code)));
            Result<AuthResponseDto>.Failure($"Falha ao criar usuário: {errors}");
        }

        var userDto = mapper.Map<UserDto>(registerDto);
        var token = jwtService.GenerateToken(userDto);
       // await userManager.AddToRoleAsync(user, user.TipoPerfil.ToString());
        var authDto = mapper.Map<AuthResponseDto>(userDto);
        authDto.Token = token;
        return Result<AuthResponseDto>.Success(authDto);
    }

    /// <summary>
    /// Verificar Login OKAY
    /// </summary>
    private async Task<Result<UsuarioModel>> VerificarLogin(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null) return Result<UsuarioModel>.Failure("Usuario ou senha incorretos");
        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);
        if (!result.Succeeded) return Result<UsuarioModel>.Failure("Usuario ou senha incorretos");

        return Result<UsuarioModel>.Success(user);
    }

}
