using Connectamente.API.Auth.DTOs;
using Connectamente.API.Auth.JwtService;
using Connectamente.API.Data;
using Connectamente.API.Helpers;
using Connectamente.API.Services.FileService;
using Connectamente.API.Usuario;
using Connectamente.API.Usuario.DTOs;
using Connectamente.API.Usuario.UsuarioService;
using Microsoft.AspNetCore.Identity;

namespace Connectamente.API.Auth.AuthService;

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
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário Inválido.");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Senha Inválida.");
        }
        user.QtdAcessos++;
        await context.SaveChangesAsync();
        var userDto = usuarioService.MapearUserDto(user);       
        var token = jwtService.GenerateToken(userDto);
        var AuthDtoMapeado = MapearAuthDto(userDto, token);
        

        return AuthDtoMapeado;       
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
        var user = CriarUsuario(registerDto, fotoPath);        
        var result = await userManager.CreateAsync(user, registerDto.Senha);
        if (!result.Succeeded)
        {
            if (fotoPath != null)
                await fileService.DeleteFileAsync(fotoPath);

            var errors = string.Join(", ", result.Errors.Select(e => TranslateIdentityErrors.TranslateErrorMessage(e.Code)));
            throw new ArgumentException($"Falha ao criar usuário: {errors}");
        }

       
        await usuarioService.CriarPerfilAuto(user);
        var userDto = usuarioService.MapearUserDto(user);        

        var token = jwtService.GenerateToken(userDto);

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
            TipoPerfil = registerDto.TipoPerfil,
            QtdAcessos = 1
        };
    }
   
}
