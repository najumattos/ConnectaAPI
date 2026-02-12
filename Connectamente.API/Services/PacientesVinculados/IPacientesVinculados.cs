using Connectamente.API.DTOs.PacienteDTOs;

namespace Connectamente.API.Services.PacientesVinculados
{
    public interface IPacientesVinculados
    {
        Task<IEnumerable<PacienteDto>> ObterPacientesVinculados(string idPsicologo);
        Task<IEnumerable<PacienteDto>> DesvincularPacientes(string idPsicologo);
        Task<IEnumerable<PacienteDto>> VincularPacientes(string idPsicologo);
    }
}
