using Connectamente.API.Data;
using Connectamente.API.Domain;
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
    public Task<Result> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosUsuarios()
    {
        throw new NotImplementedException();
    }

    public Task<Result<UserDto>> BuscarUsuarioPorId(string idUsuario)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DesativarPerfil(string idUsuario)
    {
        throw new NotImplementedException();
    }

    public UserDto MapearUserDto(UsuarioModel usuario)
    {
        throw new NotImplementedException();
    }
}