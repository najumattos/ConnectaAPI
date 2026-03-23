using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.PacienteService;

public interface IPacienteService
{
    Task<IEnumerable<FichaUsuarioDto>> BuscarTodosPacientes();
    Task<PacienteDto> BuscarPacientePorId(string idPaciente);
    Task<bool> AtualizarPaciente(string idPaciente, PacienteDto pacienteDto);
    Task<bool> ArquivarPaciente(string idPaciente);
    Task<bool> CriarPaciente(PacienteDto pacienteDto);
    Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPaciente(string idPaciente);

}
