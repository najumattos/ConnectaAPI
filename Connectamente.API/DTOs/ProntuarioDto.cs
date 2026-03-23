using Connectamente.API.Enums;

namespace Connectamente.API.DTOs
{
    public class ProntuarioDto
    {
        public string ProntuarioId { get; set; }
        public string NomePsicologo { get; set; }
        public string NomePaciente { get; set; }
        public TipoProntuarioEnum TipoProntuario { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
