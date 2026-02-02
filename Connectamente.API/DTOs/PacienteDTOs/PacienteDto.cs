using System.ComponentModel.DataAnnotations;

namespace Connectamente.API.DTOs.PacienteDTOs;

public class PacienteDto
{
    public string IdPaciente { get; set; }
    public string Email { get; set; }
    public string NomeCompleto { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string DataNascimento { get; set; }
    public string Celular { get; set; }
    public string Foto { get; set; }
    public string TipoPerfil { get; set; }
    public string PsicologoResponsavel { get; set; }
    public string ContatoEmergencia { get; set; }
    public string HistoricoPaciente { get; set; }
}
