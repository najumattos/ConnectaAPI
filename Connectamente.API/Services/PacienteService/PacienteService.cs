using Connectamente.API.Data;
using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PacienteService;

public class PacienteService(AppDbContext context, UserManager<Usuario> userManager) : IPacienteService
{
    private readonly AppDbContext _context = context;
    private readonly UserManager<Usuario> _userManager = userManager;

    public async Task<IEnumerable<UserPacienteDto>> ObterTodosPacientes()
    {
        var pacientes = await _context.Pacientes
           .AsNoTracking()
           .Include(p => p.Usuario)
           .Include(p => p.PsicologoResponsavel)
           .ThenInclude(pr => pr.Usuario)
       .ToListAsync();
        return pacientes.Select(p => MapearUserPacienteDto(p));
    }

    public async Task<UserPacienteDto> ObterPacientePorId(string idPaciente)
    {
        var paciente = await ObterDadosPaciente(idPaciente);
        if (paciente == null) return null;
        var pacienteDto = MapearUserPacienteDto(paciente);
        return pacienteDto;
    }
   
    public async Task<PacienteDto> AtualizarPaciente(string idPaciente, PacienteDto pacienteUpdateDto)
    {
        var paciente = await ObterDadosPaciente(idPaciente);
        if (paciente == null) return null;

        var pacienteDtoAtualizado =AtualizarCamposPaciente(paciente, pacienteUpdateDto);
       
      await _context.SaveChangesAsync();
       

        return pacienteDtoAtualizado;
    }

    public async Task<PacienteDto> CriarPaciente(string idFromForm, PacienteDto pacienteDto)
    {
        var paciente = new Paciente
        {
            UsuarioId = idFromForm,
            ContatoEmergencia = pacienteDto.ContatoEmergencia,
            HistoricoPaciente = pacienteDto.HistoricoPaciente,
            //  PsicologoResponsavelId = pacienteDto.PsicologoResponsavel //adicionar verificacao
        };
        var usuario = await _context.Users.FindAsync(paciente.UsuarioId);
        if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
        {
            return null;
           }

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
        var pacienteDtoAtualizado = AtualizarCamposPaciente(paciente, pacienteDto);
        return pacienteDtoAtualizado;

    }
    
    public async Task<Paciente> DeletarPaciente(string id)
    {
        var paciente = await ObterDadosPaciente(id);
        var usuario = await _userManager.FindByIdAsync(id);
        if (paciente == null && usuario == null) return null;
      
            if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
            {
                return null;
            }
            _context.Pacientes.Remove(paciente);

            usuario.TipoPerfil = Enums.TipoPerfil.PacienteDesativado;
           

            await _context.SaveChangesAsync();
            return paciente;
        
    }

    #region Métodos Auxiliares

    public async Task<Paciente> ObterDadosPaciente(string id)
    {
        return await _context.Pacientes
         .Include(p => p.Usuario)
         .Include(p => p.PsicologoResponsavel)
         .ThenInclude(pr => pr.Usuario)
         .FirstOrDefaultAsync(p => p.UsuarioId == id);
    }

    private static PacienteDto AtualizarCamposPaciente(Paciente p, PacienteDto pacienteUpdateDto)
    {

        p.ContatoEmergencia = pacienteUpdateDto.ContatoEmergencia;
        p.HistoricoPaciente = pacienteUpdateDto.HistoricoPaciente;
        // p.PsicologoResponsavelId = pacienteDto.PsicologoResponsavel;       //Esse campo se atualiza diferente

        return new PacienteDto
        {


            ContatoEmergencia = p.ContatoEmergencia,
            HistoricoPaciente = p.HistoricoPaciente,
            /*PsicologoResponsavel = p.PsicologoResponsavel?.Usuario != null
    ? $"{p.PsicologoResponsavel.Usuario.Nome} {p.PsicologoResponsavel.Usuario.Sobrenome}"
    : "Não atribuído"    */
        };
    
        
    }
    
    public UserPacienteDto MapearUserPacienteDto(Paciente p)
    {
        return new UserPacienteDto
        {
            IdPaciente = p.UsuarioId,
           /* Email = p.Usuario.Email,
            NomeCompleto = $"{p.Usuario.Nome} {p.Usuario.Sobrenome}",
            Nome = p.Usuario.Nome,
            Sobrenome = p.Usuario.Sobrenome,
            DataNascimento = p.Usuario.DataNascimento.ToString("dd/MM/yyyy"),
            Celular = p.Usuario.PhoneNumber,
            Foto = p.Usuario.Foto,
            TipoPerfil = p.Usuario.TipoPerfil.ToString(),  */

            ContatoEmergencia = p.ContatoEmergencia,
            HistoricoPaciente = p.HistoricoPaciente,
            PsicologoResponsavel = p.PsicologoResponsavel?.Usuario != null
    ? $"{p.PsicologoResponsavel.Usuario.Nome} {p.PsicologoResponsavel.Usuario.Sobrenome}"
    : "Não atribuído"
        };

    }

    public async Task<Paciente> CriarPacienteAuto(Usuario usuario)
    {
        if (usuario.TipoPerfil != Enums.TipoPerfil.Paciente)
        {
            return null;
        }
              var pacienteCriadoAuto = new Paciente
        {
            Usuario = usuario,
            UsuarioId = usuario.Id,
            ContatoEmergencia = string.Empty, 
            HistoricoPaciente = string.Empty
              };
         _context.Pacientes.Add(pacienteCriadoAuto);
        await _context.SaveChangesAsync();
        return pacienteCriadoAuto;
    }
   #endregion
}   
