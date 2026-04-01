using Connectamente.API.Domain;
using Connectamente.API.DTOs;

namespace Connectamente.API.Services.ConsultaService
{
    public interface IConsultaService
    {
        Task<Result<IEnumerable<ConsultaDto>>> BuscarTodasConsultas();
        Task<Result<ConsultaDto>> BuscarConsultaPorId(string idConsulta);
        Task<Result<bool>> EditarInfosConsulta(string idProntuario, ConsultaDto consultaDto);
        Task<Result> DeletarConsulta(string idConsulta);
        Task<Result> AgendarConsulta(ConsultaDto consultaDto);                
        Task<ConsultaDto> BuscarConsultaPorProntuario(string idProntuario);

    }
}
