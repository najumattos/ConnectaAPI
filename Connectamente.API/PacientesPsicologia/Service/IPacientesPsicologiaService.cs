using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;

namespace Connectamente.API.PacientesPsicologia.Service
{
    public interface IPacientesPsicologiaService
    {
        Task<IEnumerable<ProntuarioPacienteDto>> ObterPacientesVinculados(string idPsicologo);    //lista com todos pacientes que um psicologo atende
        Task DesvincularPaciente(string idPsicologo, string idPaciente);
        Task<ProntuarioPacienteDto> VincularPaciente(string idPsicologo, string idPaciente);    //nao sei exatamente como isso seria feito
    }
}
