namespace Connectamente.API.DTOs.RegistroSessaoDTOs
{
    public class RegistroSessaoDto
    {
        public int RegistroSessaoId { get; set; }
        public DateTime DataHoraSessao { get; set; }
        public TimeSpan DuracaoSessao { get; set; }
        public string ResumoSessao { get; set; }
        public string PacienteId { get; set; }

        public string PsicologoId { get; set; }

    }
}
