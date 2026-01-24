using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.DTOs;
using Connectamente.API.Models.PsicologoModel;

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
        .AsNoTracking()
        .Include(p => p.Usuario)
        .Include(p => p.AbordagensTerapeuticas)
        .Include(p => p.CondicoesTerapeuticas)
        .Include(p => p.TiposPacientes)
        .ToListAsync(); // Aqui os dados saem do banco e vêm para a memória

            var resultado = psicologos.Select(p => MapearParaResposta(p));

            return Ok(resultado);
        }

        // GET: api/Psicologo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Psicologo>> GetPsicologo(string id)

        {
            var psicologo = await ObterDadosPsicologo(id);
            if (psicologo == null) return NotFound();
            return Ok(MapearParaResposta(psicologo));
        }

        // PUT: api/Psicologos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPsicologo(string id, PsicologoDto psicologoDto)
        {
             var p = await ObterDadosPsicologo(id);

            if (p == null) return NotFound();

            // Atualiza campos básicos
            p.CRP = psicologoDto.CRP;
            p.Descricao = psicologoDto.Descricao;
            p.ModalidadeDeAtendimento = psicologoDto.ModalidadeDeAtendimento;

            // Atualiza Abordagens (Remove as atuais e adiciona as novas do DTO)
            _context.AbordagensPsicologo.RemoveRange(p.AbordagensTerapeuticas);
            p.AbordagensTerapeuticas = psicologoDto.AbordagensIds
                .Select(id => new AbordagensUtilizadas { AbordagemTerapeutica = (Enums.AbordagemTerapeutica)id }).ToList();

            // Atualiza Condições (Remove as atuais e adiciona as novas do DTO)
            _context.CondicoesTerapeuticas.RemoveRange(p.CondicoesTerapeuticas);
            p.CondicoesTerapeuticas = psicologoDto.CondicoesIds
                .Select(id => new CondicoesTratadas { CondicaoTerapeutica = (Enums.CondicaoTerapeutica)id }).ToList();

            // Atualiza Tipos de Paciente (Remove as atuais e adiciona as novas do DTO)
            _context.TiposPaciente.RemoveRange(p.TiposPacientes);
            p.TiposPacientes = psicologoDto.TiposPacienteIds
                .Select(id => new TiposPacienteTratados { TipoPaciente = (Enums.TipoPaciente)id }).ToList();

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
                //ta aparecendo os campos de nome e sobrenome
                UsuarioId = usuarioId,
                CRP = psicologoDto.CRP,
                Descricao = psicologoDto.Descricao,
                ModalidadeDeAtendimento = psicologoDto.ModalidadeDeAtendimento,
                // Mapeando as listas a partir dos IDs do DTO
                AbordagensTerapeuticas = psicologoDto.AbordagensIds
                .Select(id => new AbordagensUtilizadas
                { AbordagemTerapeutica = (Enums.AbordagemTerapeutica)id }).ToList(),

                CondicoesTerapeuticas = psicologoDto.CondicoesIds
                .Select(id => new CondicoesTratadas
                { CondicaoTerapeutica = (Enums.CondicaoTerapeutica)id }).ToList(),

                TiposPacientes = psicologoDto.TiposPacienteIds
                .Select(id => new TiposPacienteTratados { TipoPaciente = (Enums.TipoPaciente)id }).ToList()
            };

            _context.Psicologos.Add(psicologo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPsicologo", new { id = psicologo.UsuarioId }, psicologoDto);
        }

        // DELETE: api/Psicologos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePsicologo(string id)
        {
            var psicologo = await ObterDadosPsicologo(id);
            var usuario = await _userManager.FindByIdAsync(id);            
            if (psicologo == null && usuario == null) return NotFound();

            if (usuario != null)
            {
                var result = await _userManager.DeleteAsync(usuario);
                if (!result.Succeeded) return BadRequest(result.Errors);
            }

            return NoContent();
        }

        private bool PsicologoExists(string id)
        {
            return _context.Psicologos.Any(e => e.UsuarioId == id);
        }

        private async Task<Psicologo> ObterDadosPsicologo(string id)
        {
            return await _context.Psicologos
          .Include(p => p.Usuario)
          .Include(p => p.AbordagensTerapeuticas)
          .Include(p => p.CondicoesTerapeuticas)
          .Include(p => p.TiposPacientes)
          .FirstOrDefaultAsync(p => p.UsuarioId == id);
        }                                             
        private static object MapearParaResposta(Psicologo p)
        {
            /*O conceito de "Flattening" (Achatamento)
             Ao "trazer" esses campos do usuario(nome e foto) no DTO, você entrega um "pacote pronto". A tela de "Listagem de Psicólogos" recebe tudo o que precisa em uma única requisição.
             */
            return new
            {
                p.UsuarioId,
                p.Usuario.Nome,
                NomeCompleto = p.Usuario?.Nome + " " + p.Usuario?.Sobrenome,
                p.Usuario?.Foto,
                p.CRP,
                p.Descricao,
                p.ModalidadeDeAtendimento,
                Abordagens = p.AbordagensTerapeuticas
                    .Select(a => a.AbordagemTerapeutica.ToString()).ToList(),
                Condicoes = p.CondicoesTerapeuticas
                    .Select(c => c.CondicaoTerapeutica.ToString()).ToList(),
                Pacientes = p.TiposPacientes
                    .Select(t => t.TipoPaciente.ToString()).ToList()
            };
        }

    }
}
