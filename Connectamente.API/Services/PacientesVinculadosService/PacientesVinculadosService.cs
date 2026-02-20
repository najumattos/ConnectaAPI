using Connectamente.API.Data;
using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.Services.PsicologoService;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PacientesVinculados
{
    public class PacientesVinculadosService(AppDbContext context, IPsicologoService psicologoService, IPacienteService pacienteService) : IPacientesVinculadoService
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
                psicologo.PacientesVinculados ??= new List<Paciente>();
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
    }
}
