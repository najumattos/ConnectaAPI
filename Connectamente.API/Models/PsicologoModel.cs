using Connectamente.API.Enums;
using Connectamente.API.Usuario;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connectamente.API.Models;

[Table("Psicologo")]
public class PsicologoModel
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public virtual UsuarioModel Usuario { get; set; }

    //nº de registro como psicologo
    [Required(ErrorMessage = "O numero do Registro é obrigatório")]
    public string CRP { get; set; }   // ou RA

    [Display(Name = "Sobre o Psicologo", Prompt = "Descreva você e seu trabalho"),
    StringLength(1000), Required(ErrorMessage = "Campo obrigatório")]
    public string Descricao { get; set; }
    public TipoPerfilEnum TipoPerfil { get; set; }
    public IEnumerable<ProntuarioModel> Prontuarios { get; set; }
    public IEnumerable<ConsultaModel> ConsultasAgendadas { get; set; }
}
