using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

public class QuestoesServices: IQuestoesServices{

    private readonly AppDbContext _context;

    public QuestoesServices(AppDbContext context)
    {
        _context = context;
    }
    
    private int qtdQuestoesQuiz = 2;

    public List<Questao> GetQuestoesMateria(int materia)
    {
        List <Questao>perguntas = new List<Questao>();
        List <Questao>perguntasMateria = new List<Questao>();
        List <Questao>quizPerguntas = new List<Questao>();

        perguntas = _context.questoes.ToList();

        for(int i = 0; i < perguntas.Count(); i++)
        {
            if(perguntas[i].idmateria == materia)
            {
                perguntasMateria.Add(perguntas[i]);
            }
        }

        return perguntasMateria;
    }
}