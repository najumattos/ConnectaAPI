using Connectamente.API.Data;
using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Enums;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public async Task<Psicologo> DesativarPerfilPsicologo(string idPsicologo)
    {
        var psicologo = await ObterDadosPsicologo(idPsicologo);
        var usuario = await userManager.FindByIdAsync(idPsicologo);
        if (psicologo == null || usuario == null) return null;

        if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
        {
           // context.Psicologos.Remove(psicologo);

            usuario.TipoPerfil = Enums.TipoPerfil.PsicologoDesativado;
        }
       


        await context.SaveChangesAsync();
        return psicologo;
    }

    public PsicologoDto MapearPsicologoDto(Psicologo p)
    {        
        return new PsicologoDto
        {
	    Usuario = p.Usuario,
            IdPsicologo = p.UsuarioId,
	    NomeCompleto = $"{p.Usuario.Nome} {p.Usuario.Sobrenome}",
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

    public async Task<IEnumerable<PsicologoDto>> ObterPsicologoPorNomeOuCRP(string nomeOuCRP)
    {
        var query = context.Psicologos
            .AsNoTracking()
            .Include(p => p.Usuario)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nomeOuCRP))
        {
            // 1. Quebramos a string por espaços (Ex: "Ana Mattos" vira ["Ana", "Mattos"])
            var termos = nomeOuCRP.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var termo in termos)
            {
                // 2. Para cada termo, a query deve encontrar um match em algum dos campos
                // Usamos uma variável local para o termo por conta do fechamento do foreach no LINQ
                var t = termo.Trim();

                query = query.Where(p =>
                    p.Usuario.Nome.Contains(t) ||
                    p.Usuario.Sobrenome.Contains(t) ||
                    p.CRP.Contains(t));
            }
        }

        var psicologos = await query.ToListAsync();
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

	 if (!string.IsNullOrWhiteSpace(dto.CRP))
            p.CRP = dto.CRP;

        if (dto.ModalidadeDeAtendimento.HasValue)
        {
            p.ModalidadeDeAtendimento = dto.ModalidadeDeAtendimento.Value;
        }
        p.AbordagensTerapeuticas = dto.Abordagens?.ToList() ?? p.AbordagensTerapeuticas;
        p.CondicoesTerapeuticas = dto.Condicoes?.ToList() ?? p.CondicoesTerapeuticas;
        p.TiposPacientes = dto.TiposPacientes?.ToList() ?? p.TiposPacientes;
        return new PsicologoUpdateDto
        {
	    CRP = p.CRP,
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
public async Task<IEnumerable<PsicologoDto>> ObterPsicologoFiltrados(
    List<int>? modalidadeIds, 
    List<int>? abordagemIds, 
    List<int>? condicaoIds, 
    List<int>? publicoIds)
{
    var query = context.Psicologos
        .AsNoTracking()
        .Include(p => p.Usuario)
        .AsQueryable();

    // 1. Filtragem por Modalidades (Se o psicólogo tem UMA das modalidades selecionadas)
    if (modalidadeIds != null && modalidadeIds.Any())
    {
        var modalidadesEnum = modalidadeIds.Select(id => (ModalidadeAtendimento)id).ToList();
        query = query.Where(p => modalidadesEnum.Contains(p.ModalidadeDeAtendimento));
    }

    // 2. Filtragem por Abordagens
    if (abordagemIds != null && abordagemIds.Any())
    {
        var abordagensEnum = abordagemIds.Select(id => (AbordagemTerapeutica)id).ToList();
        // Tradução: Me dê o psicólogo que tenha QUALQUER abordagem que esteja na lista de busca
        query = query.Where(p => p.AbordagensTerapeuticas.Any(a => abordagensEnum.Contains(a)));
    }

    // 3. Filtragem por Condições
    if (condicaoIds != null && condicaoIds.Any())
    {
        var condicoesEnum = condicaoIds.Select(id => (CondicaoTerapeutica)id).ToList();
        query = query.Where(p => p.CondicoesTerapeuticas.Any(c => condicoesEnum.Contains(c)));
    }

    // 4. Filtragem por Público
    if (publicoIds != null && publicoIds.Any())
    {
        var publicosEnum = publicoIds.Select(id => (TipoPaciente)id).ToList();
        query = query.Where(p => p.TiposPacientes.Any(t => publicosEnum.Contains(t)));
    }

    var psicologos = await query.ToListAsync();

    return psicologos.Select(p => MapearPsicologoDto(p));
}
}
