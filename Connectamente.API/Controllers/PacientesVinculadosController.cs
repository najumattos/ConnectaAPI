using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Services.PacientesVinculados;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesVinculadosController(IPacientesVinculadoService pacientesVinculadosService) : ControllerBase
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
