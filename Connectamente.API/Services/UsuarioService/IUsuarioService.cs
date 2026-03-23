using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Models;
using Connectamente.API.Usuario;

namespace Connectamente.API.Services.UsuarioService;

public interface IUsuarioService
{
    Task<IEnumerable<FichaUsuarioDto>> ObterTodosUsuarios();
    Task<UserDto> ObterUsuarioPorId(string idUsuario);
    Task<bool> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto);
    Task<bool> DesativarPerfil(string idUsuario);

    UserDto MapearUserDto(UsuarioModel usuario);
}
