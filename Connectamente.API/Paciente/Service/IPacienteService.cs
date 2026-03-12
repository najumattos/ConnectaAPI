using Connectamente.API.Paciente.DTOs;
using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.Usuario;

namespace Connectamente.API.Paciente.Service;

public interface IPacienteService
{
    Task<IEnumerable<ProntuarioPacienteDto>> ObterTodosPacientes(); //metodo inutil? deveria ser obtertodospacientespor psicologo?
    Task<ProntuarioPacienteDto> ObterPacientePorId(string idPaciente);
    Task<PacienteUpdateDto> AtualizarPaciente(string idPaciente, PacienteUpdateDto pacienteUpdateDto);
    Task<PacienteModel> DeletarPaciente(string idPaciente);
    ProntuarioPacienteDto MapearUserPacienteDto(PacienteModel paciente, string nomePsicoManual = null);
    Task CriarPacienteAuto(UsuarioModel usuario);
    Task<PacienteModel> ObterDadosPaciente(string id);
}
