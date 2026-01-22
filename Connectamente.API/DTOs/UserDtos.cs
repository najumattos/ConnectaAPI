using Connectamente.API.Enums;
using Connectamente.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs
{
    public class RegisterCompleteDto
    {
        public RegisterDto DadosUsuario { get; set; }
        // Rever isso aqui. 
        public PsicologoDto? DadosPsicologo { get; set; }
    }
    public class RegisterDto
    {
        [Required][EmailAddress] public string Email { get; set; }

        [Required][MinLength(6)] public string Senha { get; set; }

        [Required] public string Nome { get; set; }

        [Required] public string Sobrenome { get; set; }

        [Required] public DateOnly DataNascimento { get; set; }

        [Required] public TipoPerfil TipoPerfil { get; set; }
        public IFormFile? Foto { get; set; }

    }
    public class PsicologoDto
    {
        //do jeito que ta tem que fazer duas requisiçoes, uma pra obter os dados de usuario e outra pra obter os dados de psicologo
        public string CRP { get; set; }
        public string Descricao { get; set; }
        public ModalidadeAtendimento ModalidadeDeAtendimento { get; set; }
        public List<int> TiposPacienteIds { get; set; }
        public List<int> AbordagensIds { get; set; }
        public List<int> CondicoesIds { get; set; }
    }

    public class LoginDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }

    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NomeCompleto { get; set; }
        public string Nome { get; set; }
        public string DataNascimento { get; set; }
        public string Foto { get; set; }
        public string TipoPerfil { get; set; }
        //public string PsicologoResponsavel { get; set; }

    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public UserDto User { get; set; } = null!;
    }
}
