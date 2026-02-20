namespace Connectamente.API.DTOs.RegistroPensamentoDTOs
{
    public class RegistroPensamentoDto
    {
        public int RegistroId { get; set; }
        public DateTime DataHora { get; set; }
        public string CaminhoArquivoRegistro { get; set; }
        public string UsuarioId { get; set; }
    }
}
