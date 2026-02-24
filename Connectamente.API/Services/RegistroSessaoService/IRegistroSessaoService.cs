using Connectamente.API.DTOs.RegistroSessaoDTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.RegistroSessaoService;

public interface IRegistroSessaoService
{
    //tem que ter todas os registros de todos pacientes por psicologo.ObterTodasSessoesPorPsicologo();
    //cada psicologo tem uma lista de pacientes(obterRegistrosPorPaciente)
    //e pra cada paciente tem suas sessoes(obterRegistrosPorId)
    //esses nomes estao horriveis
    //nao tem a necessidade de ObterTodasSessoes pq é tem o obterPAciente que consequentemente ta incluso as sessoes dele

    //Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoes();
    Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoesPorPaciente(string idPaciente);
    Task<RegistroSessaoDto> ObterRegistroSessaoPorId(int idRegistroSessao);
    Task<RegistroSessaoUpdateDto> AtualizarResumoSessao(int idRegistroSessao, RegistroSessaoUpdateDto registroSessaoUpdateDto);
    Task<RegistroSessao> DeletarRegistroSessao(int idRegistroSessao);
    RegistroSessaoDto MapearRegistroSessaoDto(RegistroSessao registroSessao);    
    Task<RegistroSessao> CriarRegistroSessao(RegistroSessaoDto registroSessaoDto);
}
