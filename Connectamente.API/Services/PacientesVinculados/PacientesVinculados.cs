using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.Services.PsicologoService;

namespace Connectamente.API.Services.PacientesVinculados
{
    public class PacientesVinculados(IPsicologoService psicologoService, IPacienteService pacienteService) : IPacientesVinculados
    {
        public async Task DesvincularPaciente(string psicologoId, string pacienteId)
        {
            var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
            var paciente = await pacienteService.ObterDadosPaciente(pacienteId);
            if (psicologo == null || pacienteId == null)
            {
                psicologo.PacientesVinculados.Remove(paciente);
            }
        }

        public async Task<IEnumerable<ProntuarioPacienteDto>> ObterPacientesVinculados(string psicologoId)
        {
            var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
            if (psicologo == null || psicologo.PacientesVinculados == null)
            {
                return Enumerable.Empty<ProntuarioPacienteDto>();
            }
            // 1. Filtramos a coleção usando Where
            return psicologo.PacientesVinculados
                             .Where(p => p.PsicologoResponsavelId == psicologo.UsuarioId)
                             .Select(p => pacienteService.MapearUserPacienteDto(p));
        }

        public async Task<Paciente> ObterPacienteVinculado(string psicologoId, string pacienteId)
        {
            var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);           
            if (psicologo == null || psicologo.PacientesVinculados == null)
            {
                return null;
            }
            return psicologo.PacientesVinculados
         .FirstOrDefault(p => p.UsuarioId == pacienteId);
        }

        public async Task VincularPaciente(string psicologoId, string pacienteId)
        {
            var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
            var paciente = await pacienteService.ObterDadosPaciente(pacienteId);
            if (psicologo != null && paciente != null)
            {                
                psicologo.PacientesVinculados ??= new List<Paciente>();

                // Verifica se já não está vinculado antes de adicionar
                if (!psicologo.PacientesVinculados.Any(p => p.UsuarioId == pacienteId))
                {
                    psicologo.PacientesVinculados.Add(paciente);                   
                }
            }
        }
    }
}
