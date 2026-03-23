using Connectamente.API.DTOs;

namespace Connectamente.API.Services.ConsultaService
{
    public interface IConsultaService
    {
        Task<IEnumerable<ConsultaDto>> BuscarTodasConsultas();
        Task<ConsultaDto> BuscarConsultaPorId(string idConsulta);
        Task<bool> EditarInfosConsulta(string idProntuario, ConsultaDto consultaDto);
        Task<bool> DeletarConsulta(string idConsulta);
        Task<bool> AgendarConsulta(ConsultaDto consultaDto);
        
        
        Task<ConsultaDto> BuscarConsultaPorProntuario(string idProntuario);

    }
}
