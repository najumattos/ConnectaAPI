using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PsicologoDTOs;

public class PsicologoDto {

    [Required] public string IdPsicologo { get; set; }
    [Required] public string CRP { get; set; }
    [Required] public string Descricao { get; set; }
    [Required] public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }
    public List<string> TiposPacientes { get; set; }
    public List<string> Abordagens { get; set; }
    public List<string> Condicoes { get; set; }
}
