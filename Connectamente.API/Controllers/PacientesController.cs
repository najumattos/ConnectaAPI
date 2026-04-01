using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.Controllers;

public class PacientesController(IPacienteService service) : MainController
{

    /// <summary>
    /// Busca Todos Pacientes
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<FichaUsuarioDto>), StatusCodes.Status200OK)]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<FichaUsuarioDto>>> GetPacientes()
    {
        var resposta = await service.BuscarTodosPacientes();

        if (resposta == null)
        {
            return NotFound("Nenhum paciente encontrado");
        }
        return Ok(resposta);

    }

    /// <summary>
    /// Exibe Dados do Paciente
    /// </summary>     
    [ProducesResponseType(typeof(PacienteDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PacienteDto>> GetPaciente(string id)
    {
        var resposta = await service.BuscarPacientePorId(id);
        return resposta switch
        {
            null => NotFound("Paciente não encontrado"),
            _ => Ok(resposta)
        };
    }

    /// <summary>
    /// Adiciona Novo Paciente
    /// </summary> 
    //POST CADE???

    /// <summary>
    /// Edita Dados Do Paciente
    /// </summary> 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("multipart/form-data")]
    [HttpPut("{id}")]
    public async Task<ActionResult> PutPaciente(string id, PacienteDto pacienteDto)
    {
        var sucesso = await service.AtualizarPaciente(id, pacienteDto);
        return sucesso switch
        {
            null => NotFound("Paciente não encontrado"),
            _ => NoContent()
        };
    }

    /// <summary>
    /// Arquiva Paciente
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id}")]
    public async Task<ActionResult> ArquivarPaciente(string id)
    {
        var sucesso = await service.ArquivarPaciente(id);
        return sucesso switch
        {
            null => NotFound("Paciente não encontrado"),
            _ => NoContent()         
        };
    }

}
