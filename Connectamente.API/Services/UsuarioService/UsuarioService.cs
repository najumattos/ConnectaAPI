using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Connectamente.API.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.UsuarioService;

public class UsuarioService() : IUsuarioService
{
    public Task<bool> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DesativarPerfil(string idUsuario)
    {
        throw new NotImplementedException();
    }

    public UserDto MapearUserDto(UsuarioModel u)
    {        
        return new UserDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Sobrenome = u.Sobrenome,
            NomeCompleto = $"{u.Nome} {u.Sobrenome}",
            Foto = u.Foto,
            Celular = u.PhoneNumber,
            DataNascimento = u.DataNascimento.ToString("dd/MM/yyyy"),
            TipoModulo = u.TipoModulo.ToString()
        };
    }


    public Task<IEnumerable<FichaUsuarioDto>> BuscarTodosUsuarios()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> BuscarUsuarioPorId(string idUsuario)
    {
        throw new NotImplementedException();
    }
}