using Connectamente.API.Data;
using Connectamente.API.RegistroConsulta.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.RegistroConsulta.Services;
                                        //delete nao ta funcionando
public class RegistroConsultaService(AppDbContext context) : IRegistroConsultaService
{
    public async Task<RegistroSessaoConsultaDto> AtualizarResumoSessao(int idRegistroSessao, RegistroSessaoConsultaDto registroSessaoUpdateDto)
    {
        var sessao = await context.RegistrosConsultas
         .FirstOrDefaultAsync(r => r.RegistroSessaoId == idRegistroSessao);
        if (sessao == null) return null;
        if (!string.IsNullOrWhiteSpace(registroSessaoUpdateDto.ResumoSessao)){
            sessao.ResumoSessao = registroSessaoUpdateDto.ResumoSessao;
            await context.SaveChangesAsync();
        }
        return new RegistroSessaoConsultaDto { ResumoSessao = sessao.ResumoSessao };
    }

    public async Task<RegistroConsultaModel> CriarRegistroSessao(RegistroConsultaDto registroSessaoDto)
    {
        var registroConsulta = new RegistroConsultaModel
        {
            ResumoSessao = registroSessaoDto.ResumoSessao,
            DataHoraSessao = registroSessaoDto.DataHoraSessao,
            DuracaoSessao = registroSessaoDto.DuracaoSessao,
            PacienteId = registroSessaoDto.PacienteId,
            PsicologoId = registroSessaoDto.PsicologoId
        };
        context.RegistrosConsultas.Add(registroConsulta);
        await context.SaveChangesAsync();
        return registroConsulta;
    }

    public Task<RegistroConsultaModel> DeletarRegistroSessao(int idRegistroSessao)
    {
        //acho que nem pode deletar
        throw new NotImplementedException();
    }

    public RegistroConsultaDto MapearRegistroSessaoDto(RegistroConsultaModel registroSessao)
    {
        return new RegistroConsultaDto
        {
            ResumoSessao = registroSessao.ResumoSessao,
            DataHoraSessao = registroSessao.DataHoraSessao,
            DuracaoSessao = registroSessao.DuracaoSessao,
            PacienteId = registroSessao.PacienteId,
            PsicologoId = registroSessao.PsicologoId
        };
    }

    public async Task<RegistroConsultaDto> ObterRegistroSessaoPorId(int idRegistroSessao)
    {
        var registroSessao = await context.RegistrosConsultas
            .AsNoTracking()
            .FirstOrDefaultAsync(registroSessao => registroSessao.RegistroSessaoId == idRegistroSessao);
        if (registroSessao == null) return null;

        return MapearRegistroSessaoDto(registroSessao);
    }

    public async Task<IEnumerable<RegistroConsultaDto>> ObterTodasSessoesPorPsicologo(string psicologoId)
    {
        var sessoes = await context.RegistrosConsultas
            .Include(s => s.Paciente)
            .Include(s => s.Psicologo)
          .AsNoTracking()
          .ToListAsync();
        return sessoes.Select(s => new RegistroConsultaDto
        {
            ResumoSessao = s.ResumoSessao,
            DataHoraSessao = s.DataHoraSessao,
            DuracaoSessao = s.DuracaoSessao,
            PacienteId = s.PacienteId,
            PsicologoId = psicologoId
        });
    }

    public async Task<IEnumerable<RegistroConsultaDto>> ObterTodasSessoesPorPaciente(string pacienteId)
    {
        var sessoes = await context.RegistrosConsultas
            .Include(s => s.Paciente)
            .Include(s => s.Psicologo)
          .AsNoTracking()
          .ToListAsync();
        return sessoes.Select(s => new RegistroConsultaDto
        {
            ResumoSessao = s.ResumoSessao,
            DataHoraSessao = s.DataHoraSessao,
            DuracaoSessao = s.DuracaoSessao,
            PacienteId = pacienteId,
            PsicologoId = s.PsicologoId
        });
    }
}
