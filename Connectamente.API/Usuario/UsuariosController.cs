using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Usuario.DTOs;
using Connectamente.API.Usuario.UsuarioService;

namespace Connectamente.API.Usuario;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;

    // GET: api/Usuarios
    /*
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuarios()
    {
        var resultado = await _usuarioService.ObterTodosUsuarios();

        return Ok(resultado);
    }*/

    // GET: api/Usuarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUsuario(string id)
    {
        var usuario = await _usuarioService.ObterUsuarioPorId(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    // PUT: api/Usuarios/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> PutUsuario(string id, IFormFile? arquivo, [FromForm] UserUpdateDto usuarioUpdateDto)
    {
        try
        {
            var usuarioAtualizado = await _usuarioService.AtualizarUsuario(id, arquivo, usuarioUpdateDto);
            return Ok(usuarioAtualizado);
        }
        catch (Exception ex)
        {
          
            return BadRequest(ex.Message);
        }
    }
    // Delete: api/Usuarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DesativarPerfil(string id)
    {
        var usuario = await _usuarioService.DesativarPerfil(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE: api/Usuarios/5

    /*  [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteUsuario(string id)
      {
          var usuario = await _usuarioService.DeletarUsuario(id);
          if (usuario == null)
          {
              return NotFound();
          }
          return NoContent();
      }*/


}
