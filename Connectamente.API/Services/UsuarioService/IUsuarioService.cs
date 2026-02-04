using Connectamente.API.DTOs;
using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.UsuarioService;

public interface IUsuarioService
{    
    Task<IEnumerable<UserDto>> ObterTodosUsuarios();
    Task<UserDto> ObterUsuarioPorId(string idUsuario);
    Task<UserDto> AtualizarUsuario(string idUsuario, IFormFile arquivoFoto, UserUpdateDto usuarioUpdateDto);
    Task<Usuario> DeletarUsuario(string idUsuario);
    UserDto MapearUserDto(Usuario usuario);
    Task CriarPerfilAuto(Usuario usuario);
}
