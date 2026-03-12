using Connectamente.API.Data;
using Connectamente.API.Paciente;
using Connectamente.API.Paciente.PacienteService;
using Connectamente.API.Paciente.Service;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.Psicologo.PsicologoService;
using Connectamente.API.Psicologo.Service;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.PacientesPsicologia.Service
{
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
    }
}
