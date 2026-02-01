using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PsicologoDTOs;

public class PsicologoDto { 

    [Required] public string CRP { get; set; }
    [Required] public string Descricao { get; set; }
    [Required] public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }
    public List<int> TiposPacienteIds { get; set; }
    public List<int> AbordagensIds { get; set; }
    public List<int> CondicoesIds { get; set; }
}
