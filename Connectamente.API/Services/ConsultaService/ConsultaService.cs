using Connectamente.API.DTOs;

namespace Connectamente.API.Services.ConsultaService
{
    public class ConsultaService : IConsultaService
    {
        public Task<bool> AgendarConsulta(ConsultaDto consultaDto)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultaDto> BuscarConsultaPorId(string idConsulta)
        {
            throw new NotImplementedException();
        }

        public Task<ConsultaDto> BuscarConsultaPorProntuario(string idProntuario)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ConsultaDto>> BuscarTodasConsultas()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletarConsulta(string idConsulta)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditarInfosConsulta(string idProntuario, ConsultaDto consultaDto)
        {
            throw new NotImplementedException();
        }
    }
}
