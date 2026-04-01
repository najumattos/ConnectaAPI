using Connectamente.API.Enums;
using Connectamente.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs;

public class PsicologoDto
{
    public string NomeCompleto { get; set; }
    public string CRP { get; set; }
    public string Descricao { get; set; }
    public TipoPerfilEnum TipoPerfil { get; set; }

}
