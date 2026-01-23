using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.Models.Paciente.Paciente
{
    [Table("RegistroSessao")]
    public class RegistroSessao
    {
        [Key]
        public int RegistroSessaoId { get; set; }

        [Required] public DateTime DataHoraSessao { get; set; }

        [Required] public TimeSpan DuracaoSessao { get; set; }

        [Display(Name = "Resumo da Sessão", Prompt = "Adicione aqui suas anotações sobre a sessão")]
        [StringLength(1000)]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string ResumoSessao { get; set; }
    }
}
