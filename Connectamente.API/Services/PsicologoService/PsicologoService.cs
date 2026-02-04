using Connectamente.API.Data;
using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Models;
using Connectamente.API.Models.PacienteModel;
using Connectamente.API.Models.PsicologoModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<PsicologoDto> CriarPsicologo(string idFromForm, PsicologoDto psicologoDto)
    {
        var psicologo = new Psicologo
        {
            UsuarioId = idFromForm,            
            CRP = psicologoDto.CRP,
            Descricao = psicologoDto.Descricao,
            ModalidadeDeAtendimento = psicologoDto.ModalidadeDeAtendimento,

            AbordagensTerapeuticas = psicologoDto.Abordagens.ToString()
                .Select(id => new AbordagensUtilizadas { AbordagemTerapeutica = (Enums.AbordagemTerapeutica)id }).ToList(),

            CondicoesTerapeuticas = psicologoDto.Condicoes.ToString()
            .Select(id => new CondicoesTratadas { CondicaoTerapeutica = (Enums.CondicaoTerapeutica)id }).ToList(),

            TiposPacientes = psicologoDto.TiposPacientes.ToString()
            .Select(id => new TiposPacienteTratados { TipoPaciente = (Enums.TipoPaciente)id }).ToList()


        };
        var usuario = await context.Users.FindAsync(psicologo.UsuarioId);
        if (usuario.TipoPerfil == Enums.TipoPerfil.Psicologo)
        {
            return null;
            }

        context.Psicologos.Add(psicologo);
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
            /* p.Usuario.Nome,
             NomeCompleto = p.Usuario?.Nome + " " + p.Usuario?.Sobrenome,
             p.Usuario?.Foto,  */
            CRP= p.CRP,
            Descricao= p.Descricao,
            ModalidadeDeAtendimento=p.ModalidadeDeAtendimento,
            Abordagens = p.AbordagensTerapeuticas
                    .Select(a => a.AbordagemTerapeutica.ToString()).ToList(),
            Condicoes = p.CondicoesTerapeuticas
                    .Select(c => c.CondicaoTerapeutica.ToString()).ToList(),
            TiposPacientes = p.TiposPacientes
                    .Select(t => t.TipoPaciente.ToString()).ToList()
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
         .Include(p => p.AbordagensTerapeuticas)
         .Include(p => p.CondicoesTerapeuticas)
         .Include(p => p.TiposPacientes)
         .ToListAsync(); // Aqui os dados saem do banco e vêm para a memória

        return psicologos.Select(p => MapearPsicologoDto(p));
    }
    private async Task<Psicologo> ObterDadosPsicologo(string id)
    {
        return await context.Psicologos
      .Include(p => p.Usuario)
      .Include(p => p.AbordagensTerapeuticas)
      .Include(p => p.CondicoesTerapeuticas)
      .Include(p => p.TiposPacientes)
      .FirstOrDefaultAsync(p => p.UsuarioId == id);
    }

    private PsicologoDto AtualizarCamposPsicologo(Psicologo p, PsicologoDto psicologoDto)
    {
        p.CRP = psicologoDto.CRP;
        p.Descricao = psicologoDto.Descricao;
        p.ModalidadeDeAtendimento = psicologoDto.ModalidadeDeAtendimento;

        // Atualiza Abordagens (Remove as atuais e adiciona as novas do DTO)
        context.AbordagensPsicologo.RemoveRange(p.AbordagensTerapeuticas);
        p.AbordagensTerapeuticas = psicologoDto.Abordagens.ToString()
            .Select(id => new AbordagensUtilizadas { AbordagemTerapeutica = (Enums.AbordagemTerapeutica)id }).ToList();

        // Atualiza Condições (Remove as atuais e adiciona as novas do DTO)
        context.CondicoesTerapeuticas.RemoveRange(p.CondicoesTerapeuticas);
        p.CondicoesTerapeuticas = psicologoDto.Condicoes.ToString()
            .Select(id => new CondicoesTratadas { CondicaoTerapeutica = (Enums.CondicaoTerapeutica)id }).ToList();

        // Atualiza Tipos de Paciente (Remove as atuais e adiciona as novas do DTO)
        context.TiposPaciente.RemoveRange(p.TiposPacientes);
        p.TiposPacientes = psicologoDto.TiposPacientes.ToString()
            .Select(id => new TiposPacienteTratados { TipoPaciente = (Enums.TipoPaciente)id }).ToList();

        return MapearPsicologoDto(p);
    }
}
