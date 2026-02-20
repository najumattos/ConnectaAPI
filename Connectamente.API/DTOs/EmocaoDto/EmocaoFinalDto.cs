using Connectamente.API.Enums;

namespace Connectamente.API.DTOs.EmocaoDto
{
    public class EmocaoFinalDto
    {      
        public Emocao Emocao { get; set; }
        public int IntensidadeFinal { get; set; }
        public string RegistroPensamentoId { get; set; }
    }
}
