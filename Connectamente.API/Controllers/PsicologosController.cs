using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;
using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Services.PsicologoService;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsicologosController(IPsicologoService psicologoService) : ControllerBase
    {
        

        // GET: api/Psicologos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Psicologo>>> GetPsicologos()
        {
            var resultado = await psicologoService.ObterTodosPsicologos();

            return Ok(resultado);
        }
       

        // GET: api/Psicologo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Psicologo>> GetPsicologo(string id)
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
}
