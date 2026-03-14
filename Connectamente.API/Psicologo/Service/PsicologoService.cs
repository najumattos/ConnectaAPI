using Connectamente.API.Data;
using Connectamente.API.Enums;
using Connectamente.API.Prontuarios.ProntuariosPsicologia;
using Connectamente.API.Psicologo.DTOs;
using Connectamente.API.Psicologo.Service;
using Connectamente.API.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Connectamente.API.Psicologo.PsicologoService;

public class PsicologoService(AppDbContext context, UserManager<UsuarioModel> userManager) : IPsicologoService
{
    public async Task<PsicologoUpdateDto> AtualizarPsicologo(string idPsicologo, PsicologoUpdateDto psicologoDto)
    {
        var psicologo = await ObterDadosPsicologo(idPsicologo);
        if (psicologo == null) return null;

        var psicologoDtoAtualizado = AtualizarCamposPsicologo(psicologo, psicologoDto);

        await context.SaveChangesAsync();


        return psicologoDtoAtualizado;
    }

    public async Task<PsicologoModel> DesativarPerfilPsicologo(string idPsicologo)
    {
        var psicologo = await ObterDadosPsicologo(idPsicologo);
        var usuario = await userManager.FindByIdAsync(idPsicologo);
        if (psicologo == null || usuario == null) return null;

        if (usuario.TipoPerfil == TipoPerfilEnum.Psicologo)
        {
           // context.Psicologos.Remove(psicologo);

            usuario.TipoPerfil = TipoPerfilEnum.Desativado;
        }
       


        await context.SaveChangesAsync();
        return psicologo;
    }

    public PsicologoDto MapearPsicologoDto(PsicologoModel p)
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

    public async Task<PsicologoModel> ObterDadosPsicologo(string psicologoId)
    {
        return await context.Psicologos
        .Include(p => p.Usuario)             // Para o nome do Psico)
        .Include(p => p.PacientesVinculados)  // Para a lista de pacientes
            .ThenInclude(p => p.Usuario)      // Para os nomes dos pacientes
        .FirstOrDefaultAsync(p => p.UsuarioId == psicologoId);
    }

    private PsicologoUpdateDto AtualizarCamposPsicologo(PsicologoModel p, PsicologoUpdateDto dto)
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

    public async Task CriarPsicologoAuto(UsuarioModel usuario)
    {
        var psicologoCriadoAuto = new PsicologoModel
        {
            UsuarioId = usuario.Id,
            CRP = string.Empty,
            Descricao = string.Empty,
            ModalidadeDeAtendimento = ModalidadeAtendimento.ModalidadeAtendimento,
            AbordagensTerapeuticas = new List<AbordagemTerapeutica>(),
            CondicoesTerapeuticas = new List<CondicaoTerapeutica>(),
            TiposPacientes = new List<TipoProntuarioEnum>()
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
        var publicosEnum = publicoIds.Select(id => (TipoProntuarioEnum)id).ToList();
        query = query.Where(p => p.TiposPacientes.Any(t => publicosEnum.Contains(t)));
    }

    var psicologos = await query.ToListAsync();

    return psicologos.Select(p => MapearPsicologoDto(p));
}
}
