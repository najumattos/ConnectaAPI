using Connectamente.API.DTOs.PacienteDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Services.PacienteService;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> ObterTodosPacientes();
    Task<PacienteDto> ObterPacientePorId(string idPaciente);
    Task<PacienteDto> AtualizarPaciente(string idPaciente, PacienteDto pacienteUpdateDto);
    Task<Paciente> DeletarPaciente(string idPaciente);
    PacienteDto MapearUserPacienteDto(Paciente paciente);
    Task CriarPacienteAuto(Usuario usuario);
}
