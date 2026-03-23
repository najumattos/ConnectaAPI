using Connectamente.API.DTOs;

namespace Connectamente.API.Services.ProntuarioService;

public class ProntuarioService : IProntuarioService
{
    // ProntuarioPacienteDto MapearUserPacienteDto(PacienteModel paciente, string nomePsicoManual = null);
    //  Task<PacienteModel> ObterDadosPaciente(string id);
    public Task<bool> ArquivarProntuario(string idProntuario)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AtualizarProntuario(string idProntuario, ProntuarioDto ProntuarioDto)
    {
        throw new NotImplementedException();
    }

    public Task<ProntuarioDto> BuscarProntuarioPorId(string idProntuario)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProntuarioBasicoDto>> BuscarTodosProntuarios()
    {
        throw new NotImplementedException();
    }

    public Task<bool> CriarProntuario(ProntuarioDto ProntuarioDto)
    {
        throw new NotImplementedException();
    }
}
