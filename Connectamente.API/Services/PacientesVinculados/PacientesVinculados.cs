using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Services.PacienteService;
using Connectamente.API.Services.PsicologoService;

namespace Connectamente.API.Services.PacientesVinculados
{
    public class PacientesVinculados(IPsicologoService psicologoService, IPacienteService pacienteService) : IPacientesVinculados
    {
        public Task<IEnumerable<PacienteDto>> DesvincularPacientes(string idPsicologo)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<PacienteDto>> ObterPacientesVinculados(string psicologoId)
        {
            var psicologo = await psicologoService.ObterDadosPsicologo(psicologoId);
            if (psicologo == null || psicologo.PacientesVinculados == null)
            {
                return Enumerable.Empty<PacienteDto>();
            }
            // 1. Filtramos a coleção usando Where
            return psicologo.PacientesVinculados
                             .Where(p => p.PsicologoResponsavelId == psicologo.UsuarioId)
                             .Select(p => pacienteService.MapearUserPacienteDto(p));
        }


        public Task<IEnumerable<PacienteDto>> VincularPacientes(string idPsicologo)
        {
            throw new NotImplementedException();
        }
    }
}
