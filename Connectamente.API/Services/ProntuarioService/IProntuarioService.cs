using Connectamente.API.DTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.ProntuarioService
{
    public interface IProntuarioService
    {
        Task<IEnumerable<ProntuarioBasicoDto>> BuscarTodosProntuarios();      
        Task<ProntuarioDto> BuscarProntuarioPorId(string idProntuario);
        Task<bool> AtualizarProntuario(string idProntuario, ProntuarioDto ProntuarioDto);
        Task<bool> ArquivarProntuario(string idProntuario);
        Task<bool> CriarProntuario(ProntuarioDto ProntuarioDto);

        // Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPaciente(string idPaciente);
        //        Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPsicologo(string idPsicologo);
        // Task<IEnumerable<ConsultaDto>> BuscarTodasConsultasPorProntuario();


    }
}
