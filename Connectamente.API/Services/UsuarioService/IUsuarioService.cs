using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.UsuarioService;

public interface IUsuarioService
{
    Task<UserDto> AtualizarUsuario(string idUsuario, IFormFile arquivo, UserUpdateDto usuarioUpdateDto);
    public Task<string> AtualizarFoto(Usuario usuario, IFormFile novaFoto);
    public bool UsuarioExists(string id);
    public Task<UserDto> ObterUsuarioPorId(string id);
    public Task<IEnumerable<UserDto>> ObterTodosUsuarios();
    public Task<Usuario> DeletarUsuario(string id);
    public UserDto MapearUserDto(Usuario usuario);
}
