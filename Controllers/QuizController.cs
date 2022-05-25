using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cors;

namespace QuizApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class QuizController : ControllerBase
    {
        private readonly AppDbContext _context;

        private readonly IQuestoesServices _questoesServices;

        private readonly IRespostasServices _respostasServices;

        public QuizController(AppDbContext context, IQuestoesServices questoesServices, IRespostasServices respostasServices)
        {
            _context = context;
            _questoesServices = questoesServices;
            _respostasServices = respostasServices;
        }

        // GET: api/Quiz/5
        [HttpGet("{idMateria}")]
        public ActionResult GetQuiz(string idMateria)
        {
            List <Questao>perguntas = new List<Questao>();
            List <Questao>quizPerguntas = new List<Questao>();
            List <Resposta>quizRespostas = new List<Resposta>();

            ArrayList valores = new ArrayList();

            perguntas = _questoesServices.GetQuestoesMateria(idMateria);

            if (perguntas.Count() < 10)
                return BadRequest();

            for(int i = 0; i<10; i++){
                quizPerguntas.Add(perguntas[i]);
            }

            List <int>idPerguntas = new List<int>();
            
            for(int i = 0; i < quizPerguntas.Count; i++)
            {
                idPerguntas.Add(quizPerguntas[i].id);
            }

            quizRespostas = _respostasServices.GetRespostasQuiz(idPerguntas);

            for(int i = 0; i < quizPerguntas.Count(); i++)
            {
                valores.Add(quizPerguntas[i]);

                for(int y = 0; y < quizRespostas.Count(); y++)
                {
                    if(quizRespostas[y].idquestao == quizPerguntas[i].id)
                    {
                        valores.Add(quizRespostas[y]);
                    }
                }
            }

            if (quizPerguntas == null)
            {
                return NotFound();
            }

            if (quizRespostas == null)
            {
                return NotFound();
            }

            return Ok(valores);
        }

        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _context.quizzes.Add(quiz);
            await _context.SaveChangesAsync();

            return quiz;
        }

        // GET: api/Quiz
        [HttpGet("Resultado")]
        public ActionResult<List<Quiz>> GetResultado(){
            
            List<Quiz> lista = new List<Quiz>();
            lista = _context.quizzes.ToList();

            if(lista == null){
                return NotFound();
            }

            return lista;
        }
    }
}