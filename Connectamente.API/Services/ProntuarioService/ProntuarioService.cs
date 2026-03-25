using Connectamente.API.Domain;
using Connectamente.API.DTOs;

namespace Connectamente.API.Services.ProntuarioService;

public class ProntuarioService : IProntuarioService
{
    public Task<Result> ArquivarProntuario(string idProntuario)
    {
        throw new NotImplementedException();
    }

    public Task<Result> AtualizarProntuario(string idProntuario, ProntuarioDto ProntuarioDto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ProntuarioDto>> BuscarProntuarioPorId(string idProntuario)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<ProntuarioBasicoDto>>> BuscarTodosProntuarios()
    {
        throw new NotImplementedException();
    }

    public Task<Result> CriarProntuario(ProntuarioDto ProntuarioDto)
    {
        throw new NotImplementedException();
    }
}
