using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.PacienteService;

public interface IPacienteService
{
    Task<IEnumerable<ProntuarioPacienteDto>> ObterTodosPacientes(); //metodo inutil? deveria ser obtertodospacientespor psicologo?
    Task<ProntuarioPacienteDto> ObterPacientePorId(string idPaciente);
    Task<PacienteUpdateDto> AtualizarPaciente(string idPaciente, PacienteUpdateDto pacienteUpdateDto);
    Task<Paciente> DeletarPaciente(string idPaciente);
    ProntuarioPacienteDto MapearUserPacienteDto(Paciente paciente, string nomePsicoManual = null);
    Task CriarPacienteAuto(Usuario usuario);
    Task<Paciente> ObterDadosPaciente(string id);
}
