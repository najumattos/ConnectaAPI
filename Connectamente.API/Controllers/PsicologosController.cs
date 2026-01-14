using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.DTOs;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsicologosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PsicologosController(AppDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }       

        // GET: api/Psicologos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Psicologo>>> GetPsicologos()
        {
            var psicologos = await _context.Psicologos
        .Include(p => p.Usuario)
        .Include(p => p.AbordagensTerapeuticas)
        .Include(p => p.CondicoesTerapeuticas)
        .Include(p => p.TiposPacientes)
        .Select(p => new
        {
            p.UsuarioId,
            NomeCompleto = p.Usuario.Nome + " " + p.Usuario.Sobrenome,
            p.CRP,
            p.Descricao,
            p.ModalidadeDeAtendimento,

            Abordagens = p.AbordagensTerapeuticas
            .Select(a => a.AbordagemTerapeutica.ToString()).ToList(),

            Condicoes = p.CondicoesTerapeuticas
            .Select(c => c.CondicaoTerapeutica.ToString()).ToList(),

            Pacientes = p.TiposPacientes
            .Select(t => t.TipoPaciente.ToString()).ToList()
        })
        .ToListAsync();

            return Ok(psicologos);
        }

        // GET: api/Psicologos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Psicologo>> GetPsicologo(string id)
        {
            var p = await _context.Psicologos
          .Include(p => p.Usuario)
          .Include(p => p.AbordagensTerapeuticas)
          .Include(p => p.CondicoesTerapeuticas)
          .Include(p => p.TiposPacientes)
          .FirstOrDefaultAsync(p => p.UsuarioId == id);

            if (p == null) return NotFound();
            /*            O conceito de "Flattening" (Achatamento)
             Ao "trazer" esses campos do usuario(nome e foto) no DTO, você entrega um "pacote pronto". A tela de "Listagem de Psicólogos" recebe tudo o que precisa em uma única requisição.
             */
            // Mapeamento manual para evitar o envio de Hashes e Loops
            var psicologoDto = new
            {
                p.UsuarioId,
                nome = p.Usuario?.Nome,
                nomeCompleto = $"{p.Usuario.Nome} {p.Usuario.Sobrenome}",
                p.CRP,
                p.Descricao,
                p.ModalidadeDeAtendimento,
                foto = p.Usuario?.Foto,
                Abordagens = p.AbordagensTerapeuticas.Select(a => a.AbordagemTerapeutica.ToString()).ToList(),
                Condicoes = p.CondicoesTerapeuticas.Select(c => c.CondicaoTerapeutica.ToString()).ToList(),
                Pacientes = p.TiposPacientes.Select(t => t.TipoPaciente.ToString()).ToList()
            };

            return Ok(psicologoDto);
        }

        // PUT: api/Psicologos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPsicologo(string id, PsicologoDto psicologoDto)
        {
            var psicologoNoBanco = await _context.Psicologos
          .Include(p => p.AbordagensTerapeuticas)
          .Include(p => p.CondicoesTerapeuticas)
          .Include(p => p.TiposPacientes)
          .FirstOrDefaultAsync(p => p.UsuarioId == id);

            if (psicologoNoBanco == null) return NotFound();

            // Atualiza campos básicos
            psicologoNoBanco.CRP = psicologoDto.CRP;
            psicologoNoBanco.Descricao = psicologoDto.Descricao;
            psicologoNoBanco.ModalidadeDeAtendimento = psicologoDto.ModalidadeDeAtendimento;

            // Atualiza Abordagens (Remove as atuais e adiciona as novas do DTO)
            _context.AbordagensPsicologo.RemoveRange(psicologoNoBanco.AbordagensTerapeuticas);
            psicologoNoBanco.AbordagensTerapeuticas = psicologoDto.AbordagensIds
                .Select(id => new AbordagemPsicologo { AbordagemTerapeutica = (Enums.AbordagemTerapeutica)id }).ToList();

            // Atualiza Condições (Remove as atuais e adiciona as novas do DTO)
            _context.CondicoesTerapeuticas.RemoveRange(psicologoNoBanco.CondicoesTerapeuticas);
            psicologoNoBanco.CondicoesTerapeuticas = psicologoDto.CondicoesIds
                .Select(id => new CondicaoPsicologo { CondicaoTerapeutica = (Enums.CondicaoTerapeutica)id }).ToList();

            // Atualiza Tipos de Paciente (Remove as atuais e adiciona as novas do DTO)
            _context.TiposPaciente.RemoveRange(psicologoNoBanco.TiposPacientes);
            psicologoNoBanco.TiposPacientes = psicologoDto.TiposPacienteIds
                .Select(id => new PacientePsicologo { TipoPaciente = (Enums.TipoPaciente)id }).ToList();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PsicologoExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Psicologos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Psicologo>> PostPsicologo(PsicologoDto psicologoDto, [FromQuery] string usuarioId)
        {
            if (PsicologoExists(usuarioId)) return Conflict("Este usuário já possui perfil de psicólogo.");

            var psicologo = new Psicologo
            {
                UsuarioId = usuarioId,
                CRP = psicologoDto.CRP,
                Descricao = psicologoDto.Descricao,
                ModalidadeDeAtendimento = psicologoDto.ModalidadeDeAtendimento,
                // Mapeando as listas a partir dos IDs do DTO
                AbordagensTerapeuticas = psicologoDto.AbordagensIds
                .Select(id => new AbordagemPsicologo 
                { AbordagemTerapeutica = (Enums.AbordagemTerapeutica)id }).ToList(),

                CondicoesTerapeuticas = psicologoDto.CondicoesIds
                .Select(id => new CondicaoPsicologo 
                { CondicaoTerapeutica = (Enums.CondicaoTerapeutica)id }).ToList(),

                TiposPacientes = psicologoDto.TiposPacienteIds
                .Select(id => new PacientePsicologo { TipoPaciente = (Enums.TipoPaciente)id }).ToList()
            };

            _context.Psicologos.Add(psicologo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPsicologo", new { id = psicologo.UsuarioId }, psicologoDto);
        }

        // DELETE: api/Psicologos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePsicologo(string id)
        {
            var psicologo = await _context.Psicologos
         .Include(p => p.AbordagensTerapeuticas)
         .Include(p => p.CondicoesTerapeuticas)
         .Include(p => p.TiposPacientes)
         .FirstOrDefaultAsync(p => p.UsuarioId == id);

            if (psicologo == null) return NotFound();

            // Remove as listas dependentes primeiro
            _context.AbordagensPsicologo.RemoveRange(psicologo.AbordagensTerapeuticas);
            _context.CondicoesTerapeuticas.RemoveRange(psicologo.CondicoesTerapeuticas);
            _context.TiposPaciente.RemoveRange(psicologo.TiposPacientes);

            var usuario = await _userManager.FindByIdAsync(id);

            if (psicologo == null && usuario == null) return NotFound();

            // 3. Se o psicólogo existe, removemos ele primeiro do Contexto
            if (psicologo != null)
            {
                _context.Psicologos.Remove(psicologo);
            }

            // 4. Se o usuário existe, removemos via UserManager
            if (usuario != null)
            {
                var result = await _userManager.DeleteAsync(usuario);
                if (!result.Succeeded) return BadRequest(result.Errors);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PsicologoExists(string id)
        {
            return _context.Psicologos.Any(e => e.UsuarioId == id);
        }
    }
}
