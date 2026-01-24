using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models.PacienteModel;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosSessoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrosSessoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RegistrosSessoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroSessao>>> GetRegistrosSesoes()
        {
            return await _context.RegistrosSesoes.ToListAsync();
        }

        // GET: api/RegistrosSessoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroSessao>> GetRegistroSessao(int id)
        {
            var registroSessao = await _context.RegistrosSesoes.FindAsync(id);

            if (registroSessao == null)
            {
                return NotFound();
            }

            return registroSessao;
        }

        // PUT: api/RegistrosSessoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistroSessao(int id, RegistroSessao registroSessao)
        {
            if (id != registroSessao.RegistroSessaoId)
            {
                return BadRequest();
            }

            _context.Entry(registroSessao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroSessaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RegistrosSessoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistroSessao>> PostRegistroSessao(RegistroSessao registroSessao)
        {
            _context.RegistrosSesoes.Add(registroSessao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistroSessao", new { id = registroSessao.RegistroSessaoId }, registroSessao);
        }

        // DELETE: api/RegistrosSessoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroSessao(int id)
        {
            var registroSessao = await _context.RegistrosSesoes.FindAsync(id);
            if (registroSessao == null)
            {
                return NotFound();
            }

            _context.RegistrosSesoes.Remove(registroSessao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroSessaoExists(int id)
        {
            return _context.RegistrosSesoes.Any(e => e.RegistroSessaoId == id);
        }
    }
}
