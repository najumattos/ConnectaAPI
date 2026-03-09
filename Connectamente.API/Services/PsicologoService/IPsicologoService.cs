using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;

namespace Connectamente.API.Services.PsicologoService;

    //a lógica seria parecida caso eu quiser adicionar mais profissionais (como psiquiatras)
public interface IPsicologoService
{
    Task<IEnumerable<PsicologoDto>> ObterPsicologoPorNomeOuCRP(string nomeOuCRP);    //para busca
    Task<IEnumerable<PsicologoDto>> ObterPsicologoFiltrados(List<int>? modalidadeIds, List<int>? abordagemIds, List<int>? condicaoIds, List<int>? publicoIds);    //para busca
    Task<PsicologoDto> ObterPsicologoPorId(string idPsicologo);      //para o perfil
    Task<PsicologoUpdateDto> AtualizarPsicologo(string idpsicologo, PsicologoUpdateDto psicologoDto);
    Task<Psicologo> DesativarPerfilPsicologo(string idPsicologo);
    PsicologoDto MapearPsicologoDto(Psicologo psicologo);
    Task CriarPsicologoAuto(Usuario usuario);
    Task<Psicologo> ObterDadosPsicologo(string psicologoId);
   
}
