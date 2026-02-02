using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.PacienteService;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> ObterTodosPacientes();
    Task<PacienteDto> ObterPacientePorId(string idPaciente);
    Task<PacienteUpdateDto> CriarPaciente(string idFromForm, PacienteUpdateDto pacienteDto);
    Task<PacienteUpdateDto> AtualizarPaciente(string idPaciente, PacienteUpdateDto pacienteUpdateDto);
    Task<Paciente> DeletarPaciente(string idPaciente);
    PacienteDto MapearPacienteDto(Paciente paciente);
}
