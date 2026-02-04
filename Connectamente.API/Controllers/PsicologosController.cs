using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Connectamente.API.Data;
using Connectamente.API.Models;
using Connectamente.API.Models.PsicologoModel;
using Connectamente.API.DTOs.PsicologoDTOs;
using Connectamente.API.Services.PsicologoService;

namespace Connectamente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PsicologosController(UserManager<Usuario> userManager, IPsicologoService psicologoService) : ControllerBase
    {
        

        // GET: api/Psicologos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Psicologo>>> GetPsicologos()
        {
            var resultado = await psicologoService.ObterTodosPsicologos();

            return Ok(resultado);
        }

        // GET: api/Psicologo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Psicologo>> GetPsicologo(string id)
        {
            var psicologo = await psicologoService.ObterPsicologoPorId(id);
            if (psicologo == null) return NotFound();
            return Ok(psicologo);
        }

        // PUT: api/Psicologos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> PutPsicologo(string id, PsicologoDto psicologoDto)
        {        
            var psicologo = await psicologoService.AtualizarPsicologo(id, psicologoDto);
            if (psicologo == null) return NotFound();

            return Ok(psicologo);
        }

        // DELETE: api/Psicologos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePsicologo(string id)
        {
            var psicologo = await psicologoService.DeletarPsicologo(id);
            if (psicologo == null)
            {
                return NotFound();
            }
            return NoContent();
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
