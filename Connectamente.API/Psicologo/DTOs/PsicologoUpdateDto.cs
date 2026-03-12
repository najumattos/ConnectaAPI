using Connectamente.API.Enums;
using Connectamente.API.Models.PacienteModel;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Psicologo.DTOs
{
    public class PsicologoUpdateDto
    {       
        public string Descricao { get; set; }
	public string CRP { get; set; }
        public ModalidadeAtendimento? ModalidadeDeAtendimento { get; set; }
        public List<TipoPaciente> TiposPacientes { get; set; }
        public List<AbordagemTerapeutica> Abordagens { get; set; }
        public List<CondicaoTerapeutica> Condicoes { get; set; }
    }
}
