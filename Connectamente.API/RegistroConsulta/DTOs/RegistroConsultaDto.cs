namespace Connectamente.API.RegistroConsulta.DTOs
{
    public class RegistroConsultaDto
    {
        public DateTime DataHoraSessao { get; set; }
        public TimeSpan DuracaoSessao { get; set; }
        public string ResumoSessao { get; set; }
        public string PacienteId { get; set; }

        public string PsicologoId { get; set; }

    }
}
