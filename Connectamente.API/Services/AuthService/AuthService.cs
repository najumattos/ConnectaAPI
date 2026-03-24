using Connectamente.API.Data;
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
    AppDbContext context
    ) : IAuthService
{

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await VerificarLogin(loginDto);
        var userDto = usuarioService.MapearUserDto(user);
        var token = jwtService.GenerateToken(userDto);
        await context.SaveChangesAsync();
        return MapearAuthDto(userDto, token);
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        var userDto = usuarioService.MapearUserDto(user);

        return userDto;
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
        var user = CriarUsuario(registerDto, fotoPath); //eu nao quero salvar o tipomodulo por motivo de seguranca
        var result = await userManager.CreateAsync(user, registerDto.Senha);
        if (!result.Succeeded)
        {
            if (fotoPath != null)
                await fileService.DeleteFileAsync(fotoPath);

            var errors = string.Join(", ", result.Errors.Select(e => TranslateIdentityErrors.TranslateErrorMessage(e.Code)));
            throw new ArgumentException($"Falha ao criar usuário: {errors}");
        }

        var userDto = usuarioService.MapearUserDto(user);
        var token = jwtService.GenerateToken(userDto);
       // await userManager.AddToRoleAsync(user, user.TipoPerfil.ToString());
        var AuthDto = MapearAuthDto(userDto, token);
        return AuthDto;
    }
    private static AuthResponseDto MapearAuthDto(UserDto userDto, string token)
    {

        return new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60),
            User = userDto
        };
    }
    private static UsuarioModel CriarUsuario(RegisterDto registerDto, string fotoPath)
    {

        return new UsuarioModel
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            Nome = registerDto.Nome,
            Sobrenome = registerDto.Sobrenome,
            DataNascimento = registerDto.DataNascimento,
            PhoneNumber = registerDto.Celular,
            Foto = fotoPath,
            TipoModulo = registerDto.TipoModulo
        };
    }
    private async Task<UsuarioModel> VerificarLogin(LoginDto loginDto)
    {
        // 1. Tenta buscar o usuário
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        // 2. Validação Fail-First: Se o user não existe, interrompe aqui.
        if (user == null) return null;

        // 3. Valida a senha
        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);

        // 4. Se a senha estiver errada, retorna null (ou você pode lançar uma exception customizada)
        if (!result.Succeeded) return null;

        return user;
    }

}
