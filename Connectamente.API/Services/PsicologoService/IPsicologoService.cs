using Connectamente.API.Domain;
using Connectamente.API.DTOs;
using Connectamente.API.DTOs.UsersDTOs;

namespace Connectamente.API.Services.PsicologoService;

public interface IPsicologoService
{
    Task<Result<IEnumerable<FichaUsuarioDto>>> BuscarTodosPsicologos();
    Task<Result<IEnumerable<PsicologoDto>>> BuscarPsicologoPorNomeOuCRP(string nomeOuCRP);    
    Task<Result<PsicologoDto>> BuscarPsicologoPorId(string idPsicologo);      
    Task<Result> AtualizarPsicologo(string idpsicologo, PsicologoDto psicologoDto);
    Task<Result> DesativarPerfilPsicologo(string idPsicologo);
   Task<Result<AuthResponseDto>> CriarUsuarioPsicologo(RegisterPsicologoUsuarioDto registerPsicologoUsuarioDto);
    //Task<Result<IEnumerable<ProntuarioDto>>> BuscarProntuarioPorPsicologo(string idPsicologo);
   

}
