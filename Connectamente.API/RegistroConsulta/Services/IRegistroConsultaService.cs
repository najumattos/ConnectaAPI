using Connectamente.API.RegistroConsulta.DTOs;

namespace Connectamente.API.RegistroConsulta.Services;

public interface IRegistroConsultaService
{
    //tem que ter todas os registros de todos pacientes por psicologo.ObterTodasSessoesPorPsicologo();
    //cada psicologo tem uma lista de pacientes(obterRegistrosPorPaciente)
    //e pra cada paciente tem suas sessoes(obterRegistrosPorId)
    //esses nomes estao horriveis
    //nao tem a necessidade de ObterTodasSessoes pq é tem o obterPAciente que consequentemente ta incluso as sessoes dele

    //Task<IEnumerable<RegistroConsultaDto>> ObterTodasSessoes();
    Task<IEnumerable<RegistroConsultaDto>> ObterTodasSessoesPorPaciente(string idPaciente);
    Task<RegistroConsultaDto> ObterRegistroSessaoPorId(int idRegistroSessao);
    Task<RegistroSessaoConsultaDto> AtualizarResumoSessao(int idRegistroSessao, RegistroSessaoConsultaDto registroSessaoUpdateDto);
    Task<RegistroConsultaModel> DeletarRegistroSessao(int idRegistroSessao);
    RegistroConsultaDto MapearRegistroSessaoDto(RegistroConsultaModel registroSessao);    
    Task<RegistroConsultaModel> CriarRegistroSessao(RegistroConsultaDto registroSessaoDto);
}
