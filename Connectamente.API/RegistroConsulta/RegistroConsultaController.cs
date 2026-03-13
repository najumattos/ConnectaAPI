using Connectamente.API.RegistroConsulta.DTOs;
using Connectamente.API.RegistroConsulta.Services;
using Microsoft.AspNetCore.Mvc;
namespace Connectamente.API.RegistroConsulta;

[Route("api/[controller]")]
[ApiController]
public class RegistroConsultaController(IRegistroConsultaService registroSessaoService) : ControllerBase
{        

                    

    // GET: api/RegistroConsultaModel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RegistroConsultaModel>> GetRegistroSessao(int id)
    {
        var sessao = await registroSessaoService.ObterRegistroSessaoPorId(id);
        if (sessao == null) return NotFound();
        return Ok(sessao);
    }

    // PUT: api/RegistroConsultaModel/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRegistroSessao(int id, RegistroConsultaDto registroSessaoUpdateDto)
    {
        var sessao = await registroSessaoService.AtualizarResumoSessao(id, registroSessaoUpdateDto);
        if (sessao == null) return NotFound();

        return Ok(sessao);
    }

    // POST: api/RegistroConsultaModel
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<RegistroConsultaModel>> PostRegistroSessao([FromForm] RegistroConsultaDto registroSessaoDto)
    {
        var sessao = await registroSessaoService.CriarRegistroSessao(registroSessaoDto);
        

        return CreatedAtAction("GetRegistroSessao", new { id = sessao.RegistroSessaoId }, sessao);
    }

    // DELETE: api/RegistroConsultaModel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRegistroSessao(int id)
    {
        await registroSessaoService.DeletarRegistroSessao(id);
        return NoContent();
    }      
}
