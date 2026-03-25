using Connectamente.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs
{
    public class RegisterPsicologoUsuarioDto
    {
        //usuario
        [Required][EmailAddress] public string Email { get; set; }

        [Required][MinLength(6)] public string Senha { get; set; }

        [Required] public string Nome { get; set; }

        [Required] public string Sobrenome { get; set; }

        [Required] public DateOnly DataNascimento { get; set; }

        [Required] public TipoModuloEnum TipoModulo { get; set; }

        [Required][Phone] public string Celular { get; set; }

        public IFormFile? Foto { get; set; }

        //psicologo
        [Required] public string CRP { get; set; }

        [Required] public TipoPerfilEnum TipoPerfil { get; set; }

        [Required] public string Descricao { get; set; }

    }
}
