using Connectamente.API.Data;
using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PacienteService;

public class PacienteService() : IPacienteService
{
    public Task<Result> ArquivarPaciente(string idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task<Result> AtualizarPaciente(string idPaciente, PacienteDto pacienteDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PacienteDto>> BuscarPacientePorId(string idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosPacientes()
    {
        throw new NotImplementedException();
    }

    public Task<Result> CriarPaciente(PacienteDto pacienteDto)
    {
        throw new NotImplementedException();
    }
}
