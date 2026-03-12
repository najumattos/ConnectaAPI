using Connectamente.API.PacientesPsicologia.Service;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.PacientesPsicologia
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesPsicologiaController(IPacientesPsicologiaService pacientesVinculadosService) : ControllerBase
    {
        // GET: api/PacientesVinculados
        [HttpGet("{psicologoId}")]
        public async Task<ActionResult<IEnumerable<ProntuarioPacienteDto>>> GetPacientesVinculados(string psicologoId)
        {
            var resultado = await pacientesVinculadosService.ObterPacientesVinculados(psicologoId);

            return Ok(resultado);
        }     

        //PUT
        [HttpPut("{idPsicologo}/{idPaciente}")]
        public async Task<IActionResult> VincularPaciente(string idPsicologo, string idPaciente) 
        {
            await pacientesVinculadosService.VincularPaciente(idPsicologo, idPaciente);
            return Ok();
        }

        //DELETE
        [HttpDelete("{idPsicologo}/{idPaciente}")]
        public async Task<IActionResult> DesvincularPaciente(string idPsicologo, string idPaciente)
        {
            await pacientesVinculadosService.DesvincularPaciente(idPsicologo, idPaciente);
            return Ok();
        }

    }
}
