using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.PacienteService;

public interface IPacienteService
{
    Task<IEnumerable<UserPacienteDto>> ObterTodosPacientes();
    Task<UserPacienteDto> ObterPacientePorId(string idPaciente);
    Task<PacienteDto> CriarPaciente(string idFromForm, PacienteDto pacienteDto);
    Task<PacienteDto> AtualizarPaciente(string idPaciente, PacienteDto pacienteUpdateDto);
    Task<Paciente> DeletarPaciente(string idPaciente);
    UserPacienteDto MapearUserPacienteDto(Paciente paciente);
    Task<Paciente> CriarPacienteAuto(Usuario usuario);
}
