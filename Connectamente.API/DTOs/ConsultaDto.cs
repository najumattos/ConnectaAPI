namespace Connectamente.API.DTOs
{
    public class ConsultaDto
    {
        public string ConsultaId { get; set; }
        public DateTime DataConsulta { get; set; }
        public TimeSpan DuracaoConsulta { get; set; }
        public string AnotacoesConsulta { get; set; }
        public string NomePaciente { get; set; }
        public string NomePsicologo { get; set; }
    }
}
