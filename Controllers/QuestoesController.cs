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

namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IRespostasServices _respostasServices;

        private readonly IQuestoesServices _questoesServices;

        public QuestoesController(AppDbContext context, IRespostasServices respostasServices, IQuestoesServices questoesServices)
        {
            _context = context;
            _respostasServices = respostasServices;
            _questoesServices = questoesServices;
        }

        // GET: api/Questoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questao>>> GetQuestoes()
        {
            return await _context.questoes.ToListAsync();
        }

        // GET: api/Questoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questao>> GetQuestao(int id)
        {
            var questao = await _context.questoes.FindAsync(id);

            if (questao == null)
            {
                return NotFound();
            }

            return questao;
        }

        [HttpGet("Materia/{idMateria}")]
        public ActionResult<List<Resposta>> GetQuestoesMateria(int idMateria)
        {
            List <Questao>listaQuestoes = new List<Questao>();

            listaQuestoes = _questoesServices.GetQuestoesMateria(idMateria);

            if (listaQuestoes == null)
            {
                return NotFound();
            }

            return Ok(listaQuestoes.ToList());
        }

        // PUT: api/Questoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestao(int id, Questao questao)
        {
            if (id != questao.id)
            {
                return BadRequest();
            }

            _context.Entry(questao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestaoExists(id))
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

        // POST: api/Questoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Questao>> PostQuestao(Questao questao)
        {
            _context.questoes.Add(questao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestao", new { id = questao.id }, questao);
        }

        // DELETE: api/Questoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestao(int id)
        {
            var questao = await _context.questoes.FindAsync(id);
            if (questao == null)
            {
                return NotFound();
            }

            _respostasServices.DeleteRepostas(questao.id);
            _context.questoes.Remove(questao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestaoExists(int id)
        {
            return _context.questoes.Any(e => e.id == id);
        }
    }
}
