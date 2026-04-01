using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Services.UsuarioService;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.Controllers;

public class UsuariosController(IUsuarioService usuarioService) : MainController
{

    /// <summary>
    /// Busca Todos Usuarios
    /// </summary>
   // [Auth("AdministradorSistema")] // Somente administradores do sistema podem acessar essa rota para obter a lista completa de usuários.
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetTodosUsuarios()
    {
        var usuarios = await usuarioService.BuscarTodosUsuarios();

        if (usuarios == null)
        {
            return NotFound("Nenhum usuário encontrado");
        }
        return Ok(usuarios);

    }

    /// <summary>
    /// Exibe Dados Cadastrais do Usuario
    /// </summary>     
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUsuario(string id)
    {
        var usuario = await usuarioService.BuscarUsuarioPorId(id);
        return usuario switch
        {
            null => NotFound("Usuário não encontrado"),      //404 não encontrado
            _ => Ok(usuario) //200 sucesso com UserDto como parametro  
        };
    }

    /// <summary>
    /// Atualiza Cadastro do Usuario
    /// </summary> 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("multipart/form-data")]
    [HttpPut("{id}")]
    public async Task<ActionResult> PutUsuario(string id, IFormFile foto, [FromForm] UserUpdateDto usuarioUpdateDto)
    {
        var sucesso = await usuarioService.AtualizarUsuario(id, foto, usuarioUpdateDto);
        return sucesso switch
        {
            null => NotFound("Usuário não encontrado"),
            _ => NoContent()         // 204 sucesso sem paramtro
        };
    }

    /// <summary>
    /// Desativa Acesso Usuario
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id}")]
    public async Task<ActionResult> DesativarPerfil(string id)
    {
        var sucesso = await usuarioService.DesativarPerfil(id);
        return sucesso switch
        {
            null => NotFound("Usuário não encontrado ou já desativado"),
            _ => NoContent()         // 204 sucesso sem parametro
        };
    }
}
