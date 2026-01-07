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
    public class RegistrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Registros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroPensamento>>> GetRegistroPensamentos()
        {
            return await _context.RegistroPensamentos.ToListAsync();
        }

        // GET: api/Registros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroPensamento>> GetRegistroPensamento(int id)
        {
            var registroPensamento = await _context.RegistroPensamentos.FindAsync(id);

            if (registroPensamento == null)
            {
                return NotFound();
            }

            return registroPensamento;
        }

        // PUT: api/Registros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistroPensamento(int id, RegistroPensamento registroPensamento)
        {
            if (id != registroPensamento.IdRegistro)
            {
                return BadRequest();
            }

            _context.Entry(registroPensamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistroPensamentoExists(id))
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

        // POST: api/Registros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RegistroPensamento>> PostRegistroPensamento(RegistroPensamento registroPensamento)
        {
            _context.RegistroPensamentos.Add(registroPensamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistroPensamento", new { id = registroPensamento.IdRegistro }, registroPensamento);
        }

        // DELETE: api/Registros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistroPensamento(int id)
        {
            var registroPensamento = await _context.RegistroPensamentos.FindAsync(id);
            if (registroPensamento == null)
            {
                return NotFound();
            }

            _context.RegistroPensamentos.Remove(registroPensamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RegistroPensamentoExists(int id)
        {
            return _context.RegistroPensamentos.Any(e => e.IdRegistro == id);
        }
    }
}
