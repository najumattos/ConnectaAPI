using Connectamente.API.Usuario.DTOs;

namespace Connectamente.API.Usuario.UsuarioService;

public interface IUsuarioService
{    
    Task<IEnumerable<UserDto>> ObterTodosUsuarios();
    Task<UserDto> ObterUsuarioPorId(string idUsuario);
    Task<UserDto> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto);
    Task<UsuarioModel> DeletarUsuario(string idUsuario);

    UserDto MapearUserDto(UsuarioModel usuario);
    Task CriarPerfilAuto(UsuarioModel usuario);
    Task<string> DesativarPerfil(string idUsuario);
}
