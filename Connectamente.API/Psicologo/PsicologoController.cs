using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Psicologo.DTOs;
using Connectamente.API.Psicologo.Service;

namespace Connectamente.API.Psicologo;

[Route("api/[controller]")]
[ApiController]
public class PsicologoController(IPsicologoService psicologoService) : ControllerBase
{


    // GET: api/Psicologos/filtros
    [HttpGet("filtros")]
    public async Task<ActionResult<IEnumerable<PsicologoDto>>> GetPsicologosFiltrados(
       [FromQuery] List<int> modalidadeIds, 
[FromQuery] List<int> abordagemIds,
[FromQuery] List<int> condicaoIds,
[FromQuery] List<int> publicoIds)
    {
        var resultado = await psicologoService.ObterPsicologoFiltrados(modalidadeIds, abordagemIds, condicaoIds, publicoIds);

        return Ok(resultado);
    }

    // GET: api/Psicologos/nomeOuCRP
    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<PsicologoDto>>> GetPsicologosPorNomeOuCRP([FromQuery] string nomeOuCRP)
    {
        var resultado = await psicologoService.ObterPsicologoPorNomeOuCRP(nomeOuCRP);

        return Ok(resultado);
    }


    // GET: api/PsicologoModel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PsicologoDto>> GetPsicologo(string id)
    {
        var psicologo = await psicologoService.ObterPsicologoPorId(id);
        if (psicologo == null) return NotFound();
        return Ok(psicologo);
    }

    // PUT: api/Psicologos/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> PutPsicologo(string id, [FromForm] PsicologoUpdateDto psicologoDto)
    {        
        var psicologo = await psicologoService.AtualizarPsicologo(id, psicologoDto);
        if (psicologo == null) return NotFound();

        return Ok(psicologo);
    }

    // DELETE: api/Psicologos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePsicologo(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        await psicologoService.DesativarPerfilPsicologo(id);
        return NoContent();
    }

                                  
}
