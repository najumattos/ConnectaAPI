using Connectamente.API.Prontuarios.ProntuariosPsicologia.DTOs;
using Connectamente.API.Psicologo;
using Connectamente.API.Psicologo.DTOs;
using Connectamente.API.Usuario;

namespace Connectamente.API.Prontuarios.ProntuariosPsicologia.Service
{
    public interface IProntuarioPsicologia
    {
        Task<IEnumerable<ProntuarioDto>> BuscarTodosProntuarios();
        Task<IEnumerable<ProntuarioDto>> BuscarTodosProntuariosDoDia(DateTime datahora);
        Task<IEnumerable<ProntuarioDto>> BuscarTodosProntuariosPorPsicologo(string idProntuario, string idPsicologo);
        Task<string> ArquivarProntuario(string idProntuario, string tipoProntuario);

        //Prontuario Adulto
        Task<string> AtualizarProntuarioAdulto(ProntuarioAdultolUpdateDto ProntuarioAdultolUpdateDto, string idprontuarioAdulto, DateTime dataHoraAtualizacao, string idUsuarioQueAtualizou);
        Task<ProntuarioAdultoDto> AbrirProntuarioAdulto(string idProntuarioAdulto);
        Task<string> CriarProntuarioAdulto(ProntuarioADultoDto prontuarioAdultoDto);
        
        //Prontuario Infantil
        Task<string> AtualizarProntuarioInfantil(ProntuarioInfantilUpdateDto ProntuarioInfantilUpdateDto, string idprontuarioInfantil, DateTime dataHoraAtualizacao, string idUsuarioQueAtualizou);
        Task<ProntuarioInfantilDto> AbrirProntuarioInfantil(string idProntuarioInfantil);
        Task<string> CriarProntuarioInfantil(ProntuarioInfantilDto prontuarioInfantilDto );

        //ProntuarioDto MapearProntuarioDto(ProntuarioModel psicologo);
        Task<PsicologoModel> ObterDadosPsicologo(string psicologoId);
    }
}
