using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;
using Connectamente.API.DTOs.PacienteDTOs;

namespace Connectamente.API.Services.PsicologoService;

public interface IPsicologoService
{
    Task<IEnumerable<PsicologoDto>> ObterTodosPsicologos();
    Task<PsicologoDto> ObterPsicologoPorId(string idPsicologo);
    Task<PsicologoUpdateDto> AtualizarPsicologo(string idpsicologo, PsicologoUpdateDto psicologoDto);
    Task<Psicologo> DeletarPsicologo(string idPsicologo);
    PsicologoDto MapearPsicologoDto(Psicologo psicologo);
    Task CriarPsicologoAuto(Usuario usuario);
    Task<Psicologo> ObterDadosPsicologo(string psicologoId);
   
}
