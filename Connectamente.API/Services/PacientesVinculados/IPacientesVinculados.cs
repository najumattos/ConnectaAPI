using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.PacientesVinculados
{
    public interface IPacientesVinculados
    {
        Task<IEnumerable<ProntuarioPacienteDto>> ObterPacientesVinculados(string idPsicologo);
        Task<Paciente> ObterPacienteVinculado(string psicologoId, string pacienteId);
        Task DesvincularPaciente(string idPsicologo, string idPaciente);
        Task VincularPaciente(string idPsicologo, string idPaciente);
    }
}
