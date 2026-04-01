using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.Services.PsicologoService;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.Controllers;

public class PsicologosController(IPsicologoService service) : MainController
{

    /// <summary>
    /// Busca Todos Psicologos
    /// </summary>
    [ProducesResponseType(typeof(IEnumerable<FichaUsuarioDto>), StatusCodes.Status200OK)]
    [HttpGet("Buscar")]
    public async Task<ActionResult<IEnumerable<FichaUsuarioDto>>> GetPsicologos()
    {
       /* var resposta = await service.BuscarTodosPsicologos();

        if (resposta == null)
        {
            return NotFound("Nenhum psicologo encontrado");
        }
        return Ok(resposta);        */
        Console.WriteLine("A lista ta aqui sim, eu to vendo");
        // Mock temporário enquanto o service não nasce
        var mockLista = new List<FichaUsuarioDto>
       {
           new FichaUsuarioDto { UsuarioId = "1", NomeCompleto = "Ana Julia (Mock)" },
           new FichaUsuarioDto { UsuarioId = "2", NomeCompleto = "Psicólogo de Teste"}
       };
        // Simula um delay de rede se quiser ser bem realista
        await Task.Delay(500);

        return Ok(mockLista);


    }

    /// <summary>
    /// Exibe Dados do Psicologo
    /// </summary>     
    [ProducesResponseType(typeof(PsicologoDto), StatusCodes.Status200OK)]
    [HttpGet("{id}")]
    public async Task<ActionResult<PsicologoDto>> GetPsicologo(string id)
    {
        /*    var resposta = await service.BuscarPsicologoPorId(id);
            return resposta switch
            {
                null => NotFound("Psicologo não encontrado"),
                _ => Ok(resposta)
            };             */
        Console.WriteLine($"Buscando detalhes do ID: {id}");

        // Mock temporário do objeto completo
        var mockDetalhe = new PsicologoDto
        {
            NomeCompleto = id == "1" ? "Ana Julia (Mock)" : "Psicólogo de Teste",
            CRP = "12/34567",
            Descricao = "Especialista em Terapia Cognitivo-Comportamental com foco em ansiedade.",
             };

        await Task.Delay(500); // Simula o tempo de resposta do banco

        return Ok(mockDetalhe);
    }

    /// <summary>
    /// Adiciona Novo Usuario ao modulo de Psicologo
    /// </summary> 
    [HttpPost("NovoUsuario")]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<AuthResponseDto>> CriarUsuarioPsicologo([FromForm] RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto)
    {
       

        var result = await service.CriarUsuarioPsicologo(registerPsicologoUsuarioDto);
       
        return Ok(result);
    }

    /// <summary>
    /// Edita Dados Do Psicologo
    /// </summary> 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("multipart/form-data")]
    [HttpPut("{id}")]
    public async Task<ActionResult> PutPsicologo(string id, PsicologoDto psicologoDto)
    {
        var sucesso = await service.AtualizarPsicologo(id, psicologoDto);
        return sucesso switch
        {
            null => NotFound("Psicologo não encontrado"),
            _ => NoContent()
        };
    }

    /// <summary>
    /// Desativar Perfil Psicologo
    /// </summary>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id}")]
    public async Task<ActionResult> DesativarPerfilPaciente(string id)
    {
        var sucesso = await service.DesativarPerfilPsicologo(id);
        return sucesso switch
        {
            null => NotFound("Psicologo não encontrado"),
            _ => NoContent()
        };
    }

}
