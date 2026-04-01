using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.ProntuarioService
{
    public interface IProntuarioService
    {
        Task<Result<IEnumerable<ProntuarioBasicoDto>>> BuscarTodosProntuarios();      
        Task<Result<ProntuarioDto>> BuscarProntuarioPorId(string idProntuario);
        Task<Result> AtualizarProntuario(string idProntuario, ProntuarioDto ProntuarioDto);
        Task<Result> ArquivarProntuario(string idProntuario);
        Task<Result> CriarProntuario(ProntuarioDto ProntuarioDto);

        // Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPaciente(string idPaciente);
        //        Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPsicologo(string idPsicologo);
        // Task<IEnumerable<ConsultaDto>> BuscarTodasConsultasPorProntuario();


    }
}
