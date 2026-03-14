using Connectamente.API.Pacientes.DTOs;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.Usuario;

namespace Connectamente.API.Pacientes.Service
{
    public interface IPacientesPsicologiaService
    {
        Task<IEnumerable<ProntuarioPacienteDto>> ObterTodosPacientes(); //DASHBOARD COORDENADOR/Todos Pacientes
        Task<ProntuarioPacienteDto> ObterPacientePorId(string idPaciente); //DASHBOARD PSICOLOGO e COORDENADOR
        Task<PacienteUpdateDto> AtualizarPaciente(string idPaciente, PacienteUpdateDto pacienteUpdateDto); //DASHBOARD PSICOLOGO e COORDENADOR
        Task<PacienteModel> DeletarPaciente(string idPaciente);
        ProntuarioPacienteDto MapearUserPacienteDto(PacienteModel paciente, string nomePsicoManual = null);
        Task CriarPacienteAuto(UsuarioModel usuario);
        Task<PacienteModel> ObterDadosPaciente(string id);
        Task<IEnumerable<ProntuarioPacienteDto>> ObterPacientesVinculados(string idPsicologo);    //lista com todos pacientes que um psicologo atende
        Task DesvincularPaciente(string idPsicologo, string idPaciente);
        Task<ProntuarioPacienteDto> VincularPaciente(string idPsicologo, string idPaciente);    //nao sei exatamente como isso seria feito
    }
}
