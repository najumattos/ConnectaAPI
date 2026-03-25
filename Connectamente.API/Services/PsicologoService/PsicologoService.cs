using Connectamente.API.Data;
using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;
using Connectamente.API.Helpers;
using Connectamente.API.Models;
using Connectamente.API.Services.FileService;
using Connectamente.API.Services.JwtService;
using Connectamente.API.Services.UsuarioService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.PsicologoService;

public class PsicologoService() : IPsicologoService
{
    public Task<Result> AtualizarPsicologo(string idpsicologo, PsicologoDto psicologoDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PsicologoDto>> BuscarPsicologoPorId(string idPsicologo)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<PsicologoDto>>> BuscarPsicologoPorNomeOuCRP(string nomeOuCRP)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosPsicologos()
    {
        throw new NotImplementedException();
    }

    public Task<Result<AuthPsicologoDto>> CriarUsuarioPsicologo(RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DesativarPerfilPsicologo(string idPsicologo)
    {
        throw new NotImplementedException();
    }
}
