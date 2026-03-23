using Connectamente.API.Data;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PsicologoService;

public class PsicologoService() : IPsicologoService
{          //PsicologoDto MapearPsicologoDto(Psicologo psicologo);
           // Task<Psicologo> ObterDadosPsicologo(string psicologoId);
    public Task<bool> AtualizarPsicologo(string idpsicologo, PsicologoDto psicologoDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPsicologo(string idPsicologo)
    {
        throw new NotImplementedException();
    }

    public Task<PsicologoDto> BuscarPsicologoPorId(string idPsicologo)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PsicologoDto>> BuscarPsicologoPorNomeOuCRP(string nomeOuCRP)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FichaUsuarioDto>> BuscarTodosPsicologos()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DesativarPerfilPsicologo(string idPsicologo)
    {
        throw new NotImplementedException();
    }
}
