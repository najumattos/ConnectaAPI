using Connectamente.API.Data;
using Connectamente.API.Paciente.PacienteService;
using Connectamente.API.Paciente.Service;
using Connectamente.API.PacientesPsicologia.DTOs;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.Psicologo.PsicologoService;
using Connectamente.API.Psicologo.Service;
using Connectamente.API.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.PacientesPsicologia.Service;

public class PacientesPsicologiaService(AppDbContext context, IPsicologoService psicologoService, IPacienteService pacienteService) : IPacientesPsicologiaService
{
    public async Task DesvincularPaciente(string psicologoId, string pacienteId)
    {
        var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
        var paciente = await pacienteService.ObterDadosPaciente(pacienteId);
        if (psicologo != null || pacienteId != null)
        {
            psicologo.PacientesVinculados.Remove(paciente);
            paciente.PsicologoResponsavelId = null;
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProntuarioPacienteDto>> ObterPacientesVinculados(string psicologoId)
    {
        var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
        if (psicologo == null || psicologo.PacientesVinculados == null)
        {
            return Enumerable.Empty<ProntuarioPacienteDto>();
        }
        var nomeDoPsicologo = psicologo.Usuario?.Nome;
        Console.WriteLine($"DEBUG: Nome extraído do psicólogo: '{nomeDoPsicologo}'");
        // 1. Filtramos a coleção usando Where
        return psicologo.PacientesVinculados
                 .Where(p => p.PsicologoResponsavelId == psicologo.UsuarioId)
                 .Select(p => pacienteService.MapearUserPacienteDto(p, nomeDoPsicologo)); // Passamos o nome aqui
    }
    public async Task<ProntuarioPacienteDto> VincularPaciente(string psicologoId, string pacienteId)
    {
        var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
        var paciente = await pacienteService.ObterDadosPaciente(pacienteId);
        
        if (psicologo != null && paciente != null)
        {                
            psicologo.PacientesVinculados ??= new List<PacienteModel>();
            paciente.PsicologoResponsavel = psicologo;
            // Verifica se já não está vinculado antes de adicionar
            if (!psicologo.PacientesVinculados.Any(p => p.UsuarioId == pacienteId))
            {
                psicologo.PacientesVinculados.Add(paciente);                   
            }
        }
        var pacienteDto = pacienteService.MapearUserPacienteDto(paciente);
       await context.SaveChangesAsync();
        return pacienteDto;
    }
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

        var pacienteDtoAtualizado = AtualizarCamposPaciente(paciente, pacienteUpdateDto);

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
