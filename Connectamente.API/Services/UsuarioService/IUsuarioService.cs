using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Models;
using Connectamente.API.Usuario;

namespace Connectamente.API.Services.UsuarioService;

public interface IUsuarioService
{
    Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosUsuarios();
    Task<Result<UserDto>> BuscarUsuarioPorId(string idUsuario);
    Task<Result> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto);
    Task<Result> DesativarPerfil(string idUsuario);
    UserDto MapearUserDto(UsuarioModel usuario);
}
