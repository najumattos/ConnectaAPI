using Connectamente.API.Attributes;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Connectamente.API.Services.ProntuarioService;

namespace Connectamente.API.Controllers;

public class ProntuariosController(IProntuarioService service) : MainController
{

    /// <summary>
    /// Busca Todos Prontuarios
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<ProntuarioBasicoDto>), StatusCodes.Status200OK)]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<FichaUsuarioDto>>> GetProntuarios()
    {
        var resposta = await service.BuscarTodosProntuarios();

        if (resposta == null || !resposta.Any())
        {
            return NotFound("Nenhum prontuario encontrado");
        }
        return Ok(resposta);

    }

    /// <summary>
    /// Exibe Dados do Prontuario
    /// </summary>     
    [ProducesResponseType(typeof(ProntuarioDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProntuarioDto>> GetProntuario(string id)
    {
        var resposta = await service.BuscarProntuarioPorId(id);
        return resposta switch
        {
            null => NotFound("Prontuario não encontrado"),
            _ => Ok(resposta)
        };
    }

    /// <summary>
    /// Adiciona Novo Prontuario
    /// </summary> 
    //POST CADE???

    /// <summary>
    /// Edita Dados Do Prontuario
    /// </summary> 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("multipart/form-data")]
    [HttpPut("{id}")]
    public async Task<ActionResult> PutProntuario(string id, ProntuarioDto prontuarioDto)
    {
        var sucesso = await service.AtualizarProntuario(id, prontuarioDto);
        return sucesso switch
        {
            false => NotFound("Prontuario não encontrado"),
            true => NoContent()
        };
    }

    /// <summary>
    /// Arquivar Prontuario
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id}")]
    public async Task<ActionResult> ArquivarProntuario(string id)
    {
        var sucesso = await service.ArquivarProntuario(id);
        return sucesso switch
        {
            false => NotFound("Prontuario não encontrado"),
            true => NoContent()
        };
    }

}
