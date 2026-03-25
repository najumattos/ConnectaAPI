using Connectamente.API.Domain;
using Connectamente.API.DTOs;

namespace Connectamente.API.Services.ConsultaService
{
    public class ConsultaService : IConsultaService
    {
        public Task<Result> AgendarConsulta(ConsultaDto consultaDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result<ConsultaDto>> BuscarConsultaPorId(string idConsulta)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultaDto> BuscarConsultaPorProntuario(string idProntuario)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<ConsultaDto>>> BuscarTodasConsultas()
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeletarConsulta(string idConsulta)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> EditarInfosConsulta(string idProntuario, ConsultaDto consultaDto)
        {
            throw new NotImplementedException();
        }
    }
}
