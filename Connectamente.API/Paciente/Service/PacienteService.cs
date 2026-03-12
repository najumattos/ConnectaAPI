using Connectamente.API.Data;
using Connectamente.API.Paciente.DTOs;
using Connectamente.API.Paciente.Service;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Paciente.PacienteService;

public class PacienteService(AppDbContext context, UserManager<UsuarioModel> userManager) : IPacienteService
{
    private readonly AppDbContext _context = context;
    private readonly UserManager<UsuarioModel> _userManager = userManager;

    public async Task<IEnumerable<ProntuarioPacienteDto>> ObterTodosPacientes()
    {
        var pacientes = await _context.Pacientes
           .AsNoTracking()
           .Include(p => p.Usuario)
           .Include(p => p.PsicologoResponsavel)
           .ThenInclude(pr => pr.Usuario)
       .ToListAsync();
        return pacientes.Select(p => MapearUserPacienteDto(p));
    }

    public async Task<ProntuarioPacienteDto> ObterPacientePorId(string idPaciente)
    {
        var paciente = await ObterDadosPaciente(idPaciente);
        if (paciente == null) return null;
        var pacienteDto = MapearUserPacienteDto(paciente);
        return pacienteDto;
    }
   
    public async Task<PacienteUpdateDto> AtualizarPaciente(string idPaciente, PacienteUpdateDto pacienteUpdateDto)
    {
        var paciente = await ObterDadosPaciente(idPaciente);
        if (paciente == null) return null;

        var pacienteDtoAtualizado =AtualizarCamposPaciente(paciente, pacienteUpdateDto);
       
      await _context.SaveChangesAsync();
       

        return pacienteDtoAtualizado;
    }

   public async Task<PacienteModel> DeletarPaciente(string id)
    {
        var paciente = await ObterDadosPaciente(id);
        var usuario = await _userManager.FindByIdAsync(id);
        if (paciente == null && usuario == null) return null;
      
            if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
            {
            //_context.Pacientes.Remove(paciente);

            usuario.TipoPerfil = Enums.TipoPerfil.Desativado;
        }
           
           

            await _context.SaveChangesAsync();
            return paciente;
        
    }

    #region Métodos Auxiliares

    public async Task<PacienteModel> ObterDadosPaciente(string id)
    {
        return await _context.Pacientes
         .Include(p => p.Usuario)
         .Include(p => p.PsicologoResponsavel)
         .ThenInclude(pr => pr.Usuario)
         .FirstOrDefaultAsync(p => p.UsuarioId == id);
    }

    private static PacienteUpdateDto AtualizarCamposPaciente(PacienteModel p, PacienteUpdateDto pacienteUpdateDto)
    {
        if (!string.IsNullOrWhiteSpace(pacienteUpdateDto.ContatoEmergencia))
            p.ContatoEmergencia = pacienteUpdateDto.ContatoEmergencia;

        if (!string.IsNullOrWhiteSpace(pacienteUpdateDto.HistoricoPaciente))
            p.HistoricoPaciente = pacienteUpdateDto.HistoricoPaciente;  
        return new PacienteUpdateDto
        {


            ContatoEmergencia = p.ContatoEmergencia,
            HistoricoPaciente = p.HistoricoPaciente,
            };
    
        
    }
    
    public ProntuarioPacienteDto MapearUserPacienteDto(PacienteModel p, string nomePsicoManual = null)
    {
        return new ProntuarioPacienteDto
        {
            IdPaciente = p.UsuarioId,          
            ContatoEmergencia = p.ContatoEmergencia,
            HistoricoPaciente = p.HistoricoPaciente,
            PsicologoResponsavel = nomePsicoManual ?? p.PsicologoResponsavel?.Usuario?.Nome ?? "Psicólogo não vinculado"
        };

    }

    public async Task CriarPacienteAuto(UsuarioModel usuario)
    {
              var pacienteCriadoAuto = new PacienteModel
        {
            Usuario = usuario,
            UsuarioId = usuario.Id,
            ContatoEmergencia = string.Empty, 
            HistoricoPaciente = string.Empty
              };
         _context.Pacientes.Add(pacienteCriadoAuto);
        await _context.SaveChangesAsync();       
    }
   #endregion
}   
