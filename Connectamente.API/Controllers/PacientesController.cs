using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models;
using Microsoft.AspNetCore.Identity;
using Connectamente.API.DTOs.PacienteDTOs;

namespace Connectamente.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacientesController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly UserManager<Usuario> _userManager;

    public PacientesController(AppDbContext context, UserManager<Usuario> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: api/Pacientes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
    {
        var pacientes = await _context.Pacientes
            .AsNoTracking()
            .Include(p => p.Usuario)
            .Include(p => p.PsicologoResponsavel) 
            .ThenInclude(pr => pr.Usuario)
        .ToListAsync();

        var resultado = pacientes.Select(p => MapearParaResposta(p));

        return Ok(resultado);
    }

    // GET: api/Pacientes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Paciente>> GetPaciente(string id)
    {
        var paciente = await ObterDadosPaciente(id);
        if (paciente == null) return Conflict("Paciente n�o encontrado."); ;
        return Ok(MapearParaResposta(paciente));
    }


    // PUT: api/Pacientes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]   
    public async Task<IActionResult> PutPaciente(string id, PacienteDto pacienteDto)
    {
        var p = await ObterDadosPaciente(id);

        if (p == null) return NotFound();                                            
                  
        // Atualiza campos b�sicos
        p.ContatoEmergencia = pacienteDto.ContatoEmergencia;
       // p.QtdAcessos = pacienteDto.QtdAcessos; esse campo nao se edita
        p.HistoricoPaciente = pacienteDto.HistoricoPaciente;
       // p.PsicologoResponsavelId = pacienteDto.PsicologoResponsavel;       //Esse campo se atualiza diferente

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PacienteExists(id)) return NotFound();
            else throw;
        }

        return NoContent();
    }

    // POST: api/Pacientes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Paciente>> PostPaciente(PacienteDto pacienteDto, [FromQuery] string usuarioId)
    {      
               var paciente = new Paciente
        {
            UsuarioId = usuarioId,
            ContatoEmergencia = pacienteDto.ContatoEmergencia,
            HistoricoPaciente = pacienteDto.HistoricoPaciente,
          //  PsicologoResponsavelId = pacienteDto.PsicologoResponsavel //adicionar verificacao
        };
        var usuario = await _context.Users.FindAsync(paciente.UsuarioId);
        if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
        {
            return BadRequest("Um usu�rio com perfil de Psic�logo n�o pode possuir um perfil de Paciente.");
        }
        if (PacienteExists(usuarioId))
        {
            return Conflict("Este usu�rio j� possui um perfil de paciente cadastrado.");
        }

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPaciente", new { id = paciente.UsuarioId }, pacienteDto);


    }

    // DELETE: api/Pacientes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePaciente(string id)
    {
        var paciente = await ObterDadosPaciente(id);
        var usuario = await _userManager.FindByIdAsync(id);
        if (paciente == null && usuario == null) return NotFound();

        if (usuario != null)
        {            
            if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
            {
                return BadRequest("Paciente n�o encontrado");
            }
            var result = await _userManager.DeleteAsync(usuario);
            if (!result.Succeeded) return BadRequest(result.Errors);
        }

        return NoContent();
    }

    private bool PacienteExists(string id)
    {
        return _context.Pacientes.Any(e => e.UsuarioId == id);
    }

    private async Task<Paciente> ObterDadosPaciente(string id)
    {
        return await _context.Pacientes
         .Include(p => p.Usuario)
         .Include(p => p.PsicologoResponsavel)
         .ThenInclude(pr => pr.Usuario)
         .FirstOrDefaultAsync(p => p.UsuarioId == id);
    }
        private static object MapearParaResposta(Paciente p)
    {
     
        return new
        {
            p.UsuarioId,
            p.Usuario.Nome,
            NomeCompleto = p.Usuario?.Nome + " " + p.Usuario?.Sobrenome,
            p.Usuario?.Foto,
            p.ContatoEmergencia,
            p.HistoricoPaciente,
            PsicologoResponsavel = p.PsicologoResponsavel?.Usuario?.Nome + " " + p.PsicologoResponsavel?.Usuario?.Sobrenome                        
        };
    }
}
