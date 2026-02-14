using Connectamente.API.Enums;
using Connectamente.API.Models.PacienteModel;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PsicologoDTOs;

public class PsicologoDto {

    [Required] public string IdPsicologo { get; set; }
    [Required] public string CRP { get; set; }
    [Required] public string Descricao { get; set; }
    [Required] public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }
    [Required] public List<TipoPaciente> TiposPacientes { get; set; }
    [Required] public List<AbordagemTerapeutica> Abordagens { get; set; }
    [Required] public List<CondicaoTerapeutica> Condicoes { get; set; }
    [Required]
    public IEnumerable<Paciente> PacientesVinculados { get; set; }
}
