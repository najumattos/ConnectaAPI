using Connectamente.API.Data;
using Connectamente.API.DTOs.RegistroSessaoDTOs;
using Connectamente.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Services.RegistroSessaoService;

public class RegistroSessaoService(AppDbContext context) : IRegistroSessaoService
{
    public async Task<RegistroSessaoUpdateDto> AtualizarResumoSessao(int idRegistroSessao, RegistroSessaoUpdateDto registroSessaoUpdateDto)
    {
        var sessao = await ObterRegistroSessaoPorId(idRegistroSessao);
        if (!string.IsNullOrWhiteSpace(registroSessaoUpdateDto.ResumoSessao))
            sessao.ResumoSessao = registroSessaoUpdateDto.ResumoSessao;
        var registroSessaoAtualizado = new RegistroSessaoUpdateDto
        {
            ResumoSessao = sessao.ResumoSessao
        };
        await context.SaveChangesAsync();
        return registroSessaoAtualizado;
    }

    public async Task<RegistroSessao> CriarRegistroSessao(RegistroSessaoDto registroSessaoDto)
    {
        var registroSessao = new RegistroSessao
        {
            //id auto?
            ResumoSessao = registroSessaoDto.ResumoSessao,
            DataHoraSessao = registroSessaoDto.DataHoraSessao,
            DuracaoSessao = registroSessaoDto.DuracaoSessao,
            PacienteId = registroSessaoDto.PacienteId,
            PsicologoId = registroSessaoDto.PsicologoId
        };
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
        var registroSessao = await context.RegistrosSesoes
            .AsNoTracking()
            .FirstOrDefaultAsync(registroSessao => registroSessao.RegistroSessaoId == idRegistroSessao);
        if (registroSessao == null) return null;

        return MapearRegistroSessaoDto(registroSessao);
    }

    public async Task<IEnumerable<RegistroSessaoDto>> ObterTodasSessoes()
    {
        var sessoes = await context.RegistrosSesoes
            .Include(s => s.PacienteId)
            .Include(s => s.PsicologoId)
          .AsNoTracking()
          .ToListAsync();
        return sessoes.Select(s => MapearRegistroSessaoDto(s));
    }
}
