#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

namespace QuizApi.Controllers
{
    //[EnableCors("Professor")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RespostasController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IRespostasServices _respostasServices;

        public RespostasController(AppDbContext context, IRespostasServices respostasServices)
        {
            _context = context;
            _respostasServices = respostasServices;
        }

        // GET: api/Respostas
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<Resposta>>> GetRespostas()
        {
            return await _context.respostas.ToListAsync();
        }*/

        // GET: api/Respostas/5
        [HttpGet("{idQuestao}")]
        public ActionResult<List<Resposta>> GetResposta(int idQuestao)
        {
            List <Resposta>listaRespostas = new List<Resposta>();

            listaRespostas = _respostasServices.GetRespostasQuestao(idQuestao);

            if (listaRespostas == null)
            {
                return NotFound();
            }

            return Ok(listaRespostas.ToList());
        }

        // PUT: api/Respostas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResposta(int id, Resposta resposta)
        {
            if (id != resposta.id)
            {
                return BadRequest();
            }

            _context.Entry(resposta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RespostaExists(id))
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

        // POST: api/Respostas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Resposta>> PostResposta(Resposta []resposta)
        {
            if(resposta.Count() != 4){
                return BadRequest();
            }

            foreach(Resposta i in resposta){
                _context.respostas.Add(i);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetRespostas", new { id = resposta.ToList() }, resposta);
        }

        // DELETE: api/Respostas/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResposta(int id)
        {
            var resposta = await _context.respostas.FindAsync(id);
            if (resposta == null)
            {
                return NotFound();
            }

            _context.respostas.Remove(resposta);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool RespostaExists(int id)
        {
            return _context.respostas.Any(e => e.id == id);
        }
    }
}
