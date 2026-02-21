using Connectamente.API.DTOs.RegistroSessaoDTOs;
using Connectamente.API.Models;

namespace Connectamente.API.Services.RegistroSessaoService;

public interface IRegistroSessaoService
{
    Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoes();
    Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoesPorPaciente(string idPaciente);
    Task<RegistroSessaoDto> ObterRegistroSessaoPorId(int idRegistroSessao);
    Task<RegistroSessaoUpdateDto> AtualizarResumoSessao(int idRegistroSessao, RegistroSessaoUpdateDto registroSessaoUpdateDto);
    Task<RegistroSessao> DeletarRegistroSessao(int idRegistroSessao);
    RegistroSessaoDto MapearRegistroSessaoDto(RegistroSessao registroSessao);    
    Task<RegistroSessao> CriarRegistroSessao(RegistroSessaoDto registroSessaoDto);
}
