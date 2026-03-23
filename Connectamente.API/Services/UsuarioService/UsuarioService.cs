using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.UsuarioService;

public class UsuarioService(
    AppDbContext context,
    IFileService fileService,
    UserManager<UsuarioModel> userManager) : IUsuarioService
{
    public Task<bool> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DesativarPerfil(string idUsuario)
    {
        throw new NotImplementedException();
    }

    public UserDto MapearUserDto(UsuarioModel usuario)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FichaUsuarioDto>> ObterTodosUsuarios()
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> ObterUsuarioPorId(string idUsuario)
    {
        throw new NotImplementedException();
    }
}