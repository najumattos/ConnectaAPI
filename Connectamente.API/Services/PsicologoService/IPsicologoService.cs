using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.Services.PsicologoService;

public interface IPsicologoService
{
    Task<IEnumerable<FichaUsuarioDto>> BuscarTodosPsicologos();
    Task<IEnumerable<PsicologoDto>> BuscarPsicologoPorNomeOuCRP(string nomeOuCRP);    
    Task<PsicologoDto> BuscarPsicologoPorId(string idPsicologo);      
    Task<bool> AtualizarPsicologo(string idpsicologo, PsicologoDto psicologoDto);
    Task<bool> DesativarPerfilPsicologo(string idPsicologo);
    Task<AuthPsicologoDto> CriarUsuarioPsicologo(RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto);
    Task<IEnumerable<ProntuarioDto>> BuscarProntuarioPorPsicologo(string idPsicologo);
   

}
