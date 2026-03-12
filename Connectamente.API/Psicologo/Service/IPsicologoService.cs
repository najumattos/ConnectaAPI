
using Connectamente.API.Psicologo.DTOs;
using Connectamente.API.Usuario;

namespace Connectamente.API.Psicologo.Service;

public interface IPsicologoService
{
    Task<IEnumerable<PsicologoDto>> ObterPsicologoPorNomeOuCRP(string nomeOuCRP);    //para busca
    Task<IEnumerable<PsicologoDto>> ObterPsicologoFiltrados(List<int>? modalidadeIds, List<int>? abordagemIds, List<int>? condicaoIds, List<int>? publicoIds);    //para busca
    Task<PsicologoDto> ObterPsicologoPorId(string idPsicologo);      //para o perfil
    Task<PsicologoUpdateDto> AtualizarPsicologo(string idpsicologo, PsicologoUpdateDto psicologoDto);
    Task<PsicologoModel> DesativarPerfilPsicologo(string idPsicologo);
    PsicologoDto MapearPsicologoDto(PsicologoModel psicologo);
    Task CriarPsicologoAuto(UsuarioModel usuario);
    Task<PsicologoModel> ObterDadosPsicologo(string psicologoId);
   
}
