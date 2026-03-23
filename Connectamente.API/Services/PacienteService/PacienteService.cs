using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PacienteService;

public class PacienteService() : IPacienteService
{
    //PacienteDto MapearUserPacienteDto(PacienteModel paciente, string nomePsicoManual = null);
    //Task<PacienteModel> ObterDadosPaciente(string id);
    public Task<bool> ArquivarPaciente(string idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarPaciente(string idPaciente, PacienteDto pacienteDto)
    {
        throw new NotImplementedException();
    }

    public Task<PacienteDto> BuscarPacientePorId(string idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPaciente(string idPaciente)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FichaUsuarioDto>> BuscarTodosPacientes()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CriarPaciente(PacienteDto pacienteDto)
    {
        throw new NotImplementedException();
    }
}   
