using Connectamente.API.Data;
using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Services.AuthService;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.JwtService;
using Connectamente.API.Services.UsuarioService;
using Connectamente.API.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PsicologoService;

public class PsicologoService() : IPsicologoService
{
    public Task<Result> AtualizarPsicologo(string idpsicologo, PsicologoDto psicologoDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PsicologoDto>> BuscarPsicologoPorId(string idPsicologo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<PsicologoDto>>> BuscarPsicologoPorNomeOuCRP(string nomeOuCRP)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosPsicologos()
    {
        throw new NotImplementedException();
    }

    public Task<Result<AuthPsicologoDto>> CriarUsuarioPsicologo(RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto)
    {
        throw new NotImplementedException();
    }
                                         
    public async Task<AuthResponseDto> CriarUsuarioPsicologo(RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto)
    {              
        var registerDto = MapearRegisterDto(registerPsicologoUsuarioDto);
        var user = await authService.RegisterAsync(registerDto);
          //se tiver tudo ok ....
        var psicologoDto = MapearPsicologoRegistro(registerPsicologoUsuarioDto);        
        var userDto = usuarioService.MapearUserDto(user);
        await CriarPsicologo(psicologoDto, user.Id);
        
        var token = jwtService.GenerateToken(userDto);
        await userManager.AddToRoleAsync(user, psicologoDto.TipoPerfil.ToString());
        var authDto = MapearAuthDto(userDto, token, psicologoDto.TipoPerfil);
        return authDto;
    }
    private async Task CriarPsicologo(PsicologoDto p, string usuarioId)
    {
        var psicologo = new PsicologoModel
        {
            UsuarioId = usuarioId,
            CRP = p.CRP,
            Descricao = p.Descricao,
           TipoPerfil = p.TipoPerfil
        };
        context.Psicologos.Add(psicologo);
        await context.SaveChangesAsync();
    }
    public RegisterDto MapearRegisterDto(RegisterPsicologoUsuarioDto rp)
    {
        return new RegisterDto
        {
            Nome = rp.Nome,
            Email = rp.Email,
            Sobrenome = rp.Sobrenome,
            NomeCompleto = $"{rp.Nome} {rp.Sobrenome}",
            Foto = rp.Foto,
            Celular = rp.Celular,
            DataNascimento = rp.DataNascimento,
            TipoModulo = rp.TipoModulo 
        };
    }
    public PsicologoDto MapearPsicologoRegistro(RegisterPsicologoUsuarioDto rp)
    {
        return new PsicologoDto
        {
            CRP = rp.CRP,
            Descricao = rp.Descricao,
            TipoPerfil = rp.TipoPerfil
        };
    }
    public PsicologoDto MapearPsicologoDto(RegisterPsicologoUsuarioDto rp)
    {
        return new PsicologoDto
        {
            CRP = rp.CRP,
            Descricao = rp.Descricao,
            TipoPerfil = rp.TipoPerfil
        };
    }

    public Task<Result> DesativarPerfilPsicologo(string idPsicologo)
    {
        throw new NotImplementedException();
    }
}

