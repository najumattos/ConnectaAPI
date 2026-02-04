using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Services.PacienteService;

namespace Connectamente.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacientesController(IPacienteService pacienteService) : ControllerBase
{
    private readonly IPacienteService _pacienteService = pacienteService;
    // GET: api/Pacientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientes()
    {      
        var resultado = await _pacienteService.ObterTodosPacientes();
        return Ok(resultado);
    }

    // GET: api/Pacientes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PacienteDto>> GetPaciente(string id)
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
    public async Task<IActionResult> PutPaciente(string id, [FromForm] PacienteDto pacienteUpdateDto)
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
