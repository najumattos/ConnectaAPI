using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Attributes;

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

        if (resposta == null || !resposta.Any())
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
    [ValidarIdRoute] //somente o usuario pode editar seu proprio perfil
    [HttpPut("{id}")]
    public async Task<ActionResult> PutPaciente(string id, PacienteDto pacienteDto)
    {
        var sucesso = await service.AtualizarPaciente(id, pacienteDto);
        return sucesso switch
        {
            false => NotFound("Paciente não encontrado"),
            true => NoContent()
        };
    }

    /// <summary>
    /// Arquiva Paciente
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> ArquivarPaciente(string id)
    {
        var sucesso = await service.ArquivarPaciente(id);
        return sucesso switch
        {
            false => NotFound("Paciente não encontrado"),
            true => NoContent()         
        };
    }

}
