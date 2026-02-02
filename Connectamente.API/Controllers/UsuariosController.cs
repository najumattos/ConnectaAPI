using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Services.UsuarioService;

namespace Connectamente.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController(IUsuarioService usuarioService) : ControllerBase
{
    private readonly IUsuarioService _usuarioService = usuarioService;

    // GET: api/Usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsuarios()
    {
        var resultado = await _usuarioService.ObterTodosUsuarios();

        return Ok(resultado);
    }

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

    // POST: api/Usuarios
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //post é o registro ne, então nao faz sentido ter dois
    /*[HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (_usuarioService.UsuarioExists(usuario.Id))
            {
                return Conflict();
            }
        }

        return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
    }          */

    // DELETE: api/Usuarios/5

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(string id)
    {
        var usuario = await _usuarioService.DeletarUsuario(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
