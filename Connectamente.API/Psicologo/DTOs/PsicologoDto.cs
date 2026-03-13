using Connectamente.API.Enums;
using Connectamente.API.PacientesPsicologia;
using Connectamente.API.Usuario;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Psicologo.DTOs;

public class PsicologoDto {

    [Required] public string IdPsicologo { get; set; }
    [Required] public UsuarioModel Usuario { get; set; }
    [Required] public string NomeCompleto { get; set; }
    [Required] public string CRP { get; set; }
    [Required] public string Descricao { get; set; }
    [Required] public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }
    [Required] public List<TipoPaciente> TiposPacientes { get; set; }
    [Required] public List<AbordagemTerapeutica> Abordagens { get; set; }
    [Required] public List<CondicaoTerapeutica> Condicoes { get; set; }
    [Required]
    public IEnumerable<PacienteModel> PacientesVinculados { get; set; }
}
