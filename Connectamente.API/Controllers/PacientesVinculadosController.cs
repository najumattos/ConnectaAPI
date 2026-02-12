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
        public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientesVinculados(string psicologoId)
        {
            var resultado = await pacientesVinculadosService.ObterPacientesVinculados(psicologoId);

            return Ok(resultado);
        }
        //eu posso ter uma tabela de paciente
        //com campos que so o psicologo pode ter acesso
        //com campos que so o paciente pode ter acesso
        //??

       
    }
}
