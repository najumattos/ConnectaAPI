using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.Services.PsicologoService;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Attributes;
using Connectamente.API.Services.AuthService;

namespace Connectamente.API.Controllers;

public class PsicologosController(IPsicologoService service, IAuthService authService) : MainController
{

    /// <summary>
    /// Busca Todos Psicologos
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<FichaUsuarioDto>), StatusCodes.Status200OK)]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<FichaUsuarioDto>>> GetPsicologos()
    {
        var resposta = await service.BuscarTodosPsicologos();

        if (resposta == null || !resposta.Any())
        {
            return NotFound("Nenhum psicologo encontrado");
        }
        return Ok(resposta);

    }

    /// <summary>
    /// Exibe Dados do Psicologo
    /// </summary>     
    [ProducesResponseType(typeof(PsicologoDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PsicologoDto>> GetPsicologo(string id)
    {
        var resposta = await service.BuscarPsicologoPorId(id);
        return resposta switch
        {
            null => NotFound("Psicologo não encontrado"),
            _ => Ok(resposta)
        };
    }

    /// <summary>
    /// Adiciona Novo Usuario ao modulo de Psicologo
    /// </summary> 
    [HttpPost("NovoUsuario")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<AuthPsicologoDto>> CriarUsuarioPsicologo([FromForm] RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto)
    {
       

        var result = await service.CriarUsuarioPsicologo(registerPsicologoUsuarioDto);
       
        return Ok(result);
    }

    /// <summary>
    /// Edita Dados Do Psicologo
    /// </summary> 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("multipart/form-data")]
    [ValidarIdRoute] //somente o usuario pode editar seu proprio perfil
    [HttpPut("{id}")]
    public async Task<ActionResult> PutPsicologo(string id, PsicologoDto psicologoDto)
    {
        var sucesso = await service.AtualizarPsicologo(id, psicologoDto);
        return sucesso switch
        {
            false => NotFound("Psicologo não encontrado"),
            true => NoContent()
        };
    }

    /// <summary>
    /// Desativar Perfil Psicologo
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DesativarPerfilPaciente(string id)
    {
        var sucesso = await service.DesativarPerfilPsicologo(id);
        return sucesso switch
        {
            false => NotFound("Psicologo não encontrado"),
            true => NoContent()
        };
    }

}
