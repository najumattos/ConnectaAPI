using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.PacientesVinculados
{
    public interface IPacientesVinculadoService
    {
        Task<IEnumerable<ProntuarioPacienteDto>> ObterPacientesVinculados(string idPsicologo);
        Task DesvincularPaciente(string idPsicologo, string idPaciente);
        Task<ProntuarioPacienteDto> VincularPaciente(string idPsicologo, string idPaciente);
    }
}
