using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Connectamente.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IFileService _fileService;
    private readonly AppDbContext _context;
    public AuthService(
        UserManager<Usuario> userManager,
        SignInManager<Usuario> signInManager,
        IJwtService jwtService,
        IFileService fileService,
        AppDbContext context
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _fileService = fileService;
        _context = context;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterCompleteDto completeDto)
    {
        var registerDto = completeDto.DadosUsuario;
        var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

        if (existingUser != null)
        {
            throw new ArgumentException("Email já está em uso.");
        }

        string fotoPath = null;
        if (registerDto.Foto != null)
        {
            fotoPath = await _fileService.SaveFileAsync(registerDto.Foto, "img/usuarios");
        }

        var user = new Usuario
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            Nome = registerDto.Nome,
            Sobrenome = registerDto.Sobrenome,
            DataNascimento = registerDto.DataNascimento,
            Foto = fotoPath,
            TipoPerfil = registerDto.TipoPerfil
        };

        // 1. Cria o usuário no Identity
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await _userManager.CreateAsync(user, registerDto.Senha);
            if (!result.Succeeded)
            {
                if (fotoPath != null) await _fileService.DeleteFileAsync(fotoPath);
                var errors = string.Join(", ", result.Errors.Select(e => TranslateIdentityErrors.TranslateErrorMessage(e.Code)));
                throw new ArgumentException($"Falha ao criar usuário: {errors}");
            }

            // 2. Tratamento de Perfis
            if (user.TipoPerfil == TipoPerfil.Psicologo && completeDto.DadosPsicologo != null)
            {
                await _userManager.AddToRoleAsync(user, "Psicologo");

                var psicologo = new Psicologo
                {
                    UsuarioId = user.Id,
                    CRP = completeDto.DadosPsicologo.CRP,
                    Descricao = completeDto.DadosPsicologo.Descricao,
                    ModalidadeDeAtendimento = completeDto.DadosPsicologo.ModalidadeDeAtendimento
                };

                psicologo.AbordagensTerapeuticas = completeDto.DadosPsicologo.AbordagensIds
    .Select(id => new AbordagemPsicologo
    {
        PsicologoId = user.Id,
        AbordagemTerapeutica = (AbordagemTerapeutica)id // Cast para o Enum
    }).ToList();

                psicologo.CondicoesTerapeuticas = completeDto.DadosPsicologo.CondicoesIds
    .Select(id => new CondicaoPsicologo
    {
        PsicologoId = user.Id,
        CondicaoTerapeutica = (CondicaoTerapeutica)id // Cast para o Enum
    }).ToList();

                psicologo.TiposPacientes = completeDto.DadosPsicologo.TiposPacienteIds
     .Select(id => new PacientePsicologo
     {
         PsicologoId = user.Id,
         TipoPaciente = (TipoPaciente)id // Cast para o Enum
     }).ToList();

                _context.Psicologos.Add(psicologo);
                await _context.SaveChangesAsync();
            }
            else
            {
                // Garante que o paciente também tenha uma Role definida
                await _userManager.AddToRoleAsync(user, "Paciente");
            }

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            // Se houve erro após salvar a foto, removemos ela do disco
            if (fotoPath != null) await _fileService.DeleteFileAsync(fotoPath);
            throw;
        }

        // 3. Monta o retorno
        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            Sobrenome = user.Sobrenome,
            DataNascimento = user.DataNascimento.ToString(),
            Foto = fotoPath != null ? _fileService.GetFileUrl(fotoPath) : null,
            TipoPerfil = user.TipoPerfil.ToString()
        };

        return new AuthResponseDto
        {
            Token = _jwtService.GenerateToken(userDto),
            Expiration = DateTime.UtcNow.AddMinutes(60),
            User = userDto
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Usuário e/ou Senha Inválidos.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Senha, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Usuário e/ou Senha Inválidos.");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            DataNascimento = user.DataNascimento.ToString(),
            Foto = !string.IsNullOrEmpty(user.Foto) ? _fileService.GetFileUrl(user.Foto) : null,
            TipoPerfil = user.TipoPerfil.ToString()
        };
        var token = _jwtService.GenerateToken(userDto);

        return new AuthResponseDto
        {
            Token = token,
            Expiration = DateTime.UtcNow.AddMinutes(60),
            User = userDto
        };
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nome = user.Nome,
            DataNascimento = user.DataNascimento.ToString(),
            Foto = !string.IsNullOrEmpty(user.Foto) ? _fileService.GetFileUrl(user.Foto) : null,
            TipoPerfil = user.TipoPerfil.ToString()
        };
    }
}
