using Connectamente.API.DTOs;
using Connectamente.API.Services.ConsultaService;
using Microsoft.AspNetCore.Mvc;

namespace Connectamente.API.Controllers;

public class ConsultasController(IConsultaService service) : MainController
{
    /// <summary>
    /// Busca Todas Consultas
    /// </summary>
   [ProducesResponseType(typeof(IEnumerable<ConsultaDto>), StatusCodes.Status200OK)]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<ConsultaDto>>> GetConsultas()
    {
        var resposta = await service.BuscarTodasConsultas();

        if (resposta == null || !resposta.Any())
        {
            return NotFound("Nenhuma consulta encontrado");
        }
        return Ok(resposta);

    }

    /// <summary>
    /// Exibe Dados da Consulta
    /// </summary>     
    [ProducesResponseType(typeof(ConsultaDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ConsultaDto>> GetConsulta(string id)
    {
        var resposta = await service.BuscarConsultaPorId(id);
        return resposta switch
        {
            null => NotFound("Consulta não encontrada"),      
            _ => Ok(resposta) 
        };
    }

    /// <summary>
    /// Criar Nova Consulta
    /// </summary> 
    //POST CADE???

    /// <summary>
    /// Edita Dados Da Consulta
    /// </summary> 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("multipart/form-data")]
    [HttpPut("{id}")]
    public async Task<ActionResult> PutConsulta(string id, ConsultaDto consultaDto)
    {
        var sucesso = await service.EditarInfosConsulta(id, consultaDto);
        return sucesso switch
        {
            false => NotFound("Consulta não encontrada"),
            true => NoContent()         
        };
    }

    /// <summary>
    /// Exclui Registro de Consulta
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletarAgendamentoConsulta(string id)
    {
        var sucesso = await service.DeletarConsulta(id);
        return sucesso switch
        {
            false => NotFound("Agendamento não encontrado"),
            true => NoContent()         // 204 sucesso sem parametro
        };
    }
}
