using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbordagensController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AbordagensController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Abordagens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AbordagemTerapeutica>>> GetAbordagensTerapeuticas()
        {
            return await _context.AbordagensTerapeuticas.ToListAsync();
        }

        // GET: api/Abordagens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AbordagemTerapeutica>> GetAbordagemTerapeutica(int id)
        {
            var abordagemTerapeutica = await _context.AbordagensTerapeuticas.FindAsync(id);

            if (abordagemTerapeutica == null)
            {
                return NotFound();
            }

            return abordagemTerapeutica;
        }

        // PUT: api/Abordagens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbordagemTerapeutica(int id, AbordagemTerapeutica abordagemTerapeutica)
        {
            if (id != abordagemTerapeutica.IdAbordagemTerapeutica)
            {
                return BadRequest();
            }

            _context.Entry(abordagemTerapeutica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbordagemTerapeuticaExists(id))
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

        // POST: api/Abordagens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AbordagemTerapeutica>> PostAbordagemTerapeutica(AbordagemTerapeutica abordagemTerapeutica)
        {
            _context.AbordagensTerapeuticas.Add(abordagemTerapeutica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAbordagemTerapeutica", new { id = abordagemTerapeutica.IdAbordagemTerapeutica }, abordagemTerapeutica);
        }

        // DELETE: api/Abordagens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbordagemTerapeutica(int id)
        {
            var abordagemTerapeutica = await _context.AbordagensTerapeuticas.FindAsync(id);
            if (abordagemTerapeutica == null)
            {
                return NotFound();
            }

            _context.AbordagensTerapeuticas.Remove(abordagemTerapeutica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AbordagemTerapeuticaExists(int id)
        {
            return _context.AbordagensTerapeuticas.Any(e => e.IdAbordagemTerapeutica == id);
        }
    }
}
