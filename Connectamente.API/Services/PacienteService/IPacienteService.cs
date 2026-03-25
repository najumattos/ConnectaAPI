using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.PacienteService;

public interface IPacienteService
{
    Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosPacientes();
    Task<Result<PacienteDto>> BuscarPacientePorId(string idPaciente);
    Task<Result> AtualizarPaciente(string idPaciente, PacienteDto pacienteDto);
    Task<Result> ArquivarPaciente(string idPaciente);
    Task<Result> CriarPaciente(PacienteDto pacienteDto);
    //Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPaciente(string idPaciente);

}
