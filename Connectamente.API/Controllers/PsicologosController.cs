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
    public class PsicologosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PsicologosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Psicologos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Psicologo>>> GetPsicologos()
        {
            return await _context.Psicologos.ToListAsync();
        }

        // GET: api/Psicologos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Psicologo>> GetPsicologo(string id)
        {
            var psicologo = await _context.Psicologos.FindAsync(id);

            if (psicologo == null)
            {
                return NotFound();
            }

            return psicologo;
        }

        // PUT: api/Psicologos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPsicologo(string id, Psicologo psicologo)
        {
            if (id != psicologo.UsuarioId)
            {
                return BadRequest();
            }

            _context.Entry(psicologo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PsicologoExists(id))
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

        // POST: api/Psicologos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Psicologo>> PostPsicologo(Psicologo psicologo)
        {
            _context.Psicologos.Add(psicologo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PsicologoExists(psicologo.UsuarioId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPsicologo", new { id = psicologo.UsuarioId }, psicologo);
        }

        // DELETE: api/Psicologos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePsicologo(string id)
        {
            var psicologo = await _context.Psicologos.FindAsync(id);
            if (psicologo == null)
            {
                return NotFound();
            }

            _context.Psicologos.Remove(psicologo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PsicologoExists(string id)
        {
            return _context.Psicologos.Any(e => e.UsuarioId == id);
        }
    }
}
