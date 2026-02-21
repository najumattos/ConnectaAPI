using Connectamente.API.Data;
using Connectamente.API.DTOs.RegistroSessaoDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.RegistroSessaoService;
                                        //delete nao ta funcionando
public class RegistroSessaoService(AppDbContext context) : IRegistroSessaoService
{
    public async Task<RegistroSessaoUpdateDto> AtualizarResumoSessao(int idRegistroSessao, RegistroSessaoUpdateDto registroSessaoUpdateDto)
    {
        var sessao = await context.RegistrosSessoes
         .FirstOrDefaultAsync(r => r.RegistroSessaoId == idRegistroSessao);
        if (sessao == null) return null;
        if (!string.IsNullOrWhiteSpace(registroSessaoUpdateDto.ResumoSessao)){
            sessao.ResumoSessao = registroSessaoUpdateDto.ResumoSessao;
            await context.SaveChangesAsync();
        }
        return new RegistroSessaoUpdateDto { ResumoSessao = sessao.ResumoSessao };
    }

    public async Task<RegistroSessao> CriarRegistroSessao(RegistroSessaoDto registroSessaoDto)
    {
        var registroSessao = new RegistroSessao
        {
            ResumoSessao = registroSessaoDto.ResumoSessao,
            DataHoraSessao = registroSessaoDto.DataHoraSessao,
            DuracaoSessao = registroSessaoDto.DuracaoSessao,
            PacienteId = registroSessaoDto.PacienteId,
            PsicologoId = registroSessaoDto.PsicologoId
        };
        context.RegistrosSessoes.Add(registroSessao);
        await context.SaveChangesAsync();
        return registroSessao;
    }

    public Task<RegistroSessao> DeletarRegistroSessao(int idRegistroSessao)
    {
        //acho que nem pode deletar
        throw new NotImplementedException();
    }

    public RegistroSessaoDto MapearRegistroSessaoDto(RegistroSessao registroSessao)
    {
        return new RegistroSessaoDto
        {
            ResumoSessao = registroSessao.ResumoSessao,
            DataHoraSessao = registroSessao.DataHoraSessao,
            DuracaoSessao = registroSessao.DuracaoSessao,
            PacienteId = registroSessao.PacienteId,
            PsicologoId = registroSessao.PsicologoId
        };
    }

    public async Task<RegistroSessaoDto> ObterRegistroSessaoPorId(int idRegistroSessao)
    {
        var registroSessao = await context.RegistrosSessoes
            .AsNoTracking()
            .FirstOrDefaultAsync(registroSessao => registroSessao.RegistroSessaoId == idRegistroSessao);
        if (registroSessao == null) return null;

        return MapearRegistroSessaoDto(registroSessao);
    }

    public async Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoesPorPsicologo(string psicologoId)
    {
        var sessoes = await context.RegistrosSessoes
            .Include(s => s.Paciente)
            .Include(s => s.Psicologo)
          .AsNoTracking()
          .ToListAsync();
        return sessoes.Select(s => new RegistroSessaoDto
        {
            ResumoSessao = s.ResumoSessao,
            DataHoraSessao = s.DataHoraSessao,
            DuracaoSessao = s.DuracaoSessao,
            PacienteId = s.PacienteId,
            PsicologoId = psicologoId
        });
    }

    public async Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoesPorPaciente(string pacienteId)
    {
        var sessoes = await context.RegistrosSessoes
            .Include(s => s.Paciente)
            .Include(s => s.Psicologo)
          .AsNoTracking()
          .ToListAsync();
        return sessoes.Select(s => new RegistroSessaoDto
        {
            ResumoSessao = s.ResumoSessao,
            DataHoraSessao = s.DataHoraSessao,
            DuracaoSessao = s.DuracaoSessao,
            PacienteId = pacienteId,
            PsicologoId = s.PsicologoId
        });
    }
}
