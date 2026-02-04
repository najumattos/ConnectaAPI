using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;

namespace Connectamente.API.Services.PsicologoService;

public interface IPsicologoService
{
    Task<IEnumerable<PsicologoDto>> ObterTodosPsicologos();
    Task<PsicologoDto> ObterPsicologoPorId(string idPsicologo);
    Task<PsicologoDto> AtualizarPsicologo(string idpsicologo, PsicologoDto psicologoDto);
    Task<Psicologo> DeletarPsicologo(string idPsicologo);
    PsicologoDto MapearPsicologoDto(Psicologo psicologo);
    Task CriarPsicologoAuto(Usuario usuario);
}
