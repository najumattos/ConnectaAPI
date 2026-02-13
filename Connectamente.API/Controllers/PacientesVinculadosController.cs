using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Services.PacientesVinculados;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesVinculadosController(IPacientesVinculados pacientesVinculadosService) : ControllerBase
    {
        // GET: api/PacientesVinculados
        [HttpGet("{psicologoId}/pacientes")]
        public async Task<ActionResult<IEnumerable<ProntuarioPacienteDto>>> GetPacientesVinculados(string psicologoId)
        {
            var resultado = await pacientesVinculadosService.ObterPacientesVinculados(psicologoId);

            return Ok(resultado);
        }

        [HttpGet("{psicologoId}/{pacienteId}")]
        public async Task<ActionResult<Paciente>> GetPacienteVinculado(string psicologoId)
        {
            var resultado = await pacientesVinculadosService.ObterPacienteVinculado(psicologoId);

            return Ok(resultado);
        }

        //PUT
        [HttpPut("{idPsicologo}/vincular/{idPaciente}")]
        public async Task<IActionResult> VincularPaciente(string idPsicologo, string idPaciente) 
        {
            await pacientesVinculadosService.VincularPaciente(idPsicologo, idPaciente);
            return Ok();
        }

        //DELETE
        [HttpDelete("{idPsicologo}/desvincular/{idPaciente}")]
        public async Task<IActionResult> DesvincularPaciente(string idPsicologo, string idPaciente)
        {
            await pacientesVinculadosService.DesvincularPaciente(idPsicologo, idPaciente);
            return Ok();
        }

    }
}
