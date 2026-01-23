using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Models
{
    //Essa tabela de paciente serve para controle do psicologo.
    [Table("Paciente")]
    public class Paciente
    {
        [Key]
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }

        [Required] public string ContatoEmergencia { get; set; }

        public int QtdAcessos { get; set; }

        [Display(Name = "Histórico do Paciente", Prompt = "Informações como se ja faz acompanhamento, uso de medicacao, diagnosticos previos, sono, alimentacao, uso de substancias, atividade fisica")]
        [StringLength(1000)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string HistoricoPaciente { get; set; } 

        public int RegistroSessaoId { get; set; }
        [ForeignKey("RegistroSessaoId")]
        public virtual ICollection<RegistroSessao> RegistroSessoes { get; set; }

        public string PsicologoResponsavelId { get; set; }
        [ForeignKey("PsicologoResponsavelId")]
        public virtual Psicologo PsicologoResponsavel { get; set; }
    }
}
