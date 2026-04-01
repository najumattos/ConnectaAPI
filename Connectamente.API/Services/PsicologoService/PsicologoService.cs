using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;

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

    public Task<Result<AuthResponseDto>> CriarUsuarioPsicologo(RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DesativarPerfilPsicologo(string idPsicologo)
    {
        throw new NotImplementedException();
    }
}

