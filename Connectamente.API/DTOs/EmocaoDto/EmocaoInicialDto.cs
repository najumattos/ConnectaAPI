using Connectamente.API.Enums;

namespace Connectamente.API.DTOs.EmocaoDto
{
    public class EmocaoInicialDto
    {
        public Emocao Emocao { get; set; }
        public int IntensidadeInicial { get; set; }
        public string RegistroPensamentoId { get; set; }
    }
}
