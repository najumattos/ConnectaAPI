using Connectamente.API.Data;
using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;
using Connectamente.API.Services.PacienteService;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Connectamente.API.Services.PsicologoService;

public class PsicologoService(AppDbContext context, UserManager<Usuario> userManager) : IPsicologoService
{
    public async Task<PsicologoDto> AtualizarPsicologo(string idPsicologo, PsicologoDto psicologoDto)
    {
        var psicologo = await ObterDadosPsicologo(idPsicologo);
        if (psicologo == null) return null;

        var psicologoDtoAtualizado = AtualizarCamposPsicologo(psicologo, psicologoDto);

        await context.SaveChangesAsync();


        return psicologoDtoAtualizado;
    }

    public async Task<Psicologo> DeletarPsicologo(string idPsicologo)
    {
        var psicologo = await ObterDadosPsicologo(idPsicologo);
        var usuario = await userManager.FindByIdAsync(idPsicologo);
        if (psicologo == null && usuario == null) return null;

        if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
        {
            return null;
        }
        context.Psicologos.Remove(psicologo);

        usuario.TipoPerfil = Enums.TipoPerfil.PsicologoDesativado;


        await context.SaveChangesAsync();
        return psicologo;
    }

    public PsicologoDto MapearPsicologoDto(Psicologo p)
    {
        //parei aqui. Tem que mexer no post tambem
        return new PsicologoDto
        {
            IdPsicologo = p.UsuarioId,
            CRP = p.CRP,
            Descricao = p.Descricao,
            ModalidadeDeAtendimento = p.ModalidadeDeAtendimento,
            Condicoes = [.. p.CondicoesTerapeuticas],
            Abordagens = [.. p.AbordagensTerapeuticas],
            TiposPacientes = [.. p.TiposPacientes]
        };
    }

    public async Task<PsicologoDto> ObterPsicologoPorId(string idPsicologo)
    {
        var psicologo = await ObterDadosPsicologo(idPsicologo);
        if (psicologo == null) return null;
        var psicologoDto = MapearPsicologoDto(psicologo);
        return psicologoDto;
    }

    public async Task<IEnumerable<PsicologoDto>> ObterTodosPsicologos()
    {
        var psicologos = await context.Psicologos
          .AsNoTracking()
          .Include(p => p.Usuario)
          .ToListAsync(); // Aqui os dados saem do banco e vêm para a memória

        return psicologos.Select(p => MapearPsicologoDto(p));
    }
    public async Task<Psicologo> ObterDadosPsicologo(string psicologoId)
    {
        return await context.Psicologos
        .Include(p => p.PacientesVinculados) // ESSENCIAL para não vir null
            .ThenInclude(p => p.Usuario)      // Se precisar de dados do usuário do paciente
        .FirstOrDefaultAsync(p => p.UsuarioId == psicologoId);
    }

    private PsicologoDto AtualizarCamposPsicologo(Psicologo p, PsicologoDto dto)
    {
        p.CRP = !string.IsNullOrWhiteSpace(dto.CRP) ? dto.CRP : p.CRP;
        p.Descricao = !string.IsNullOrWhiteSpace(dto.Descricao) ? dto.Descricao : p.Descricao;
        p.ModalidadeDeAtendimento = dto.ModalidadeDeAtendimento;
        if (dto.Abordagens != null && dto.Abordagens.Any())
        {
            p.AbordagensTerapeuticas = dto.Abordagens.ToList();
        }

        if (dto.Condicoes != null && dto.Condicoes.Any())
        {
            p.CondicoesTerapeuticas = dto.Condicoes.ToList();
        }

        if (dto.TiposPacientes != null && dto.TiposPacientes.Any())
        {
            p.TiposPacientes = dto.TiposPacientes.ToList();
        }
        return MapearPsicologoDto(p);
    }

    public async Task CriarPsicologoAuto(Usuario usuario)
    {
        var psicologoCriadoAuto = new Psicologo
        {
            UsuarioId = usuario.Id,
            CRP = string.Empty,
            Descricao = string.Empty,
            ModalidadeDeAtendimento = ModalidadeAtendimento.ModalidadeAtendimento,
            AbordagensTerapeuticas = new List<AbordagemTerapeutica>(),
            CondicoesTerapeuticas = new List<CondicaoTerapeutica>(),
            TiposPacientes = new List<TipoPaciente>()
        };
        context.Psicologos.Add(psicologoCriadoAuto);
        await context.SaveChangesAsync();
    }

}
