using Connectamente.API.DTOs.RegistroSessaoDTOs;
using Connectamente.API.Models;
using Connectamente.API.Services.RegistroSessaoService;
using Microsoft.AspNetCore.Mvc;
namespace Connectamente.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegistroSessaoController(IRegistroSessaoService registroSessaoService) : ControllerBase
{        

    // GET: api/RegistroSessao
 /*   [HttpGet]
    public async Task<ActionResult<IEnumerable<RegistroSessao>>> GetRegistrosSesoes()
    {
        var resultado = await registroSessaoService.ObterTodasSessoes();

        return Ok(resultado);
    }                      */

    // GET: api/RegistroSessao/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroSessao>> GetRegistroSessao(int id)
    {
        var sessao = await registroSessaoService.ObterRegistroSessaoPorId(id);
        if (sessao == null) return NotFound();
        return Ok(sessao);
    }

    // PUT: api/RegistroSessao/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistroSessao(int id, RegistroSessaoUpdateDto registroSessaoUpdateDto)
    {
        var sessao = await registroSessaoService.AtualizarResumoSessao(id, registroSessaoUpdateDto);
        if (sessao == null) return NotFound();

        return Ok(sessao);
    }

    // POST: api/RegistroSessao
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<RegistroSessao>> PostRegistroSessao([FromForm] RegistroSessaoDto registroSessaoDto)
    {
        var sessao = await registroSessaoService.CriarRegistroSessao(registroSessaoDto);
        

        return CreatedAtAction("GetRegistroSessao", new { id = sessao.RegistroSessaoId }, sessao);
    }

    // DELETE: api/RegistroSessao/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistroSessao(int id)
    {
        await registroSessaoService.DeletarRegistroSessao(id);
        return NoContent();
    }      
}
