namespace Connectamente.API.DTOs.UsersDTOs
{
    public class FichaUsuarioDto
    {
        public string UsuarioId { get; set; }
        public string NomeCompleto { get; set; }
        public string TipoPerfil { get; set; }
        public DateTime ProximaConsulta { get; set; }
        public DateTime UltimaConsulta { get; set; }
        // botao ver prontuarios e ver consultas
    }
}
