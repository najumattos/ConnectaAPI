using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Paciente.Service;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.PacientesPsicologia.DTOs;

namespace Connectamente.API.PacientesPsicologia;

[Route("api/[controller]")]
[ApiController]
public class PacientesController(IPacienteService pacienteService) : ControllerBase
{
    private readonly IPacienteService _pacienteService = pacienteService;     

    // GET: api/Pacientes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProntuarioPacienteDto>> GetPaciente(string id)
    {
        var pacienteDto = await _pacienteService.ObterPacientePorId(id);
        if (pacienteDto == null)
        {
            return NotFound();
        }
        return Ok(pacienteDto);
    }         

     // PUT: api/Pacientes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> PutPaciente(string id, [FromForm] PacienteUpdateDto pacienteUpdateDto)
    {
        var paciente = await _pacienteService.AtualizarPaciente(id, pacienteUpdateDto);

        if (paciente == null) return NotFound();                                            
                         
        return Ok(paciente);
    }
 
    // DELETE: api/Pacientes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaciente(string id)
    {
        var paciente = await _pacienteService.DeletarPaciente(id);
        if (paciente == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
