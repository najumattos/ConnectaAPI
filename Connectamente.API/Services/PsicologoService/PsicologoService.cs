using Connectamente.API.Data;
using Connectamente.API.DTOs.PacienteDTOs;
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
    public async Task<PsicologoUpdateDto> AtualizarPsicologo(string idPsicologo, PsicologoUpdateDto psicologoDto)
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
        .Include(p => p.Usuario)             // Para o nome do Psico)
        .Include(p => p.PacientesVinculados)  // Para a lista de pacientes
            .ThenInclude(p => p.Usuario)      // Para os nomes dos pacientes
        .FirstOrDefaultAsync(p => p.UsuarioId == psicologoId);
    }

    private PsicologoUpdateDto AtualizarCamposPsicologo(Psicologo p, PsicologoUpdateDto dto)
    {
        if (!string.IsNullOrWhiteSpace(dto.Descricao))
            p.Descricao = dto.Descricao;

        if (dto.ModalidadeDeAtendimento.HasValue)
        {
            p.ModalidadeDeAtendimento = dto.ModalidadeDeAtendimento.Value;
        }
        p.AbordagensTerapeuticas = dto.Abordagens?.ToList() ?? p.AbordagensTerapeuticas;
        p.CondicoesTerapeuticas = dto.Condicoes?.ToList() ?? p.CondicoesTerapeuticas;
        p.TiposPacientes = dto.TiposPacientes?.ToList() ?? p.TiposPacientes;
        return new PsicologoUpdateDto
        {
            Descricao = p.Descricao,
            ModalidadeDeAtendimento = p.ModalidadeDeAtendimento,
            Condicoes = [.. p.CondicoesTerapeuticas],
            Abordagens = [.. p.AbordagensTerapeuticas],
            TiposPacientes = [.. p.TiposPacientes]
        };
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
