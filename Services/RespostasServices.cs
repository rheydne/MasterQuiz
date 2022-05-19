using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApi.Models;

public class RespostasServices: IRespostasServices{

    private readonly AppDbContext _context;

    public RespostasServices(AppDbContext context)
    {
        _context = context;
    }

    private int qtdRespostasQuiz = 4;
    
    public List<Resposta> GetRespostasQuestao(int idQuestao){

        List <Resposta>respostas = new List<Resposta>();
        List <Resposta>listaRespostas = new List<Resposta>();
        respostas = _context.respostas.ToList();           
        
        for(int i = 0; i < respostas.Count(); i++)
        {
            if(respostas[i].idquestao == idQuestao && listaRespostas.Count() < qtdRespostasQuiz)
            {
                listaRespostas.Add(respostas[i]);
            }
        }
        return listaRespostas;
    }

    public List<Resposta> GetRespostasQuiz(List <int> id){

        List <Resposta>respostas = new List<Resposta>();
        List <Resposta>quizRespostas = new List<Resposta>();  

        respostas = _context.respostas.ToList();

        for(int i = 0; i < id.Count(); i++)
        {
            for(int y = 0; y < respostas.Count(); y++)
            {
                if(respostas[y].idquestao == id[i])
                {
                    quizRespostas.Add(respostas[y]);
                }
            }
        }          
        return quizRespostas;
    }

    public void DeleteRepostas(int id){

        List <Resposta>respostas = new List<Resposta>();

        respostas = _context.respostas.ToList();

        foreach(Resposta i in respostas)
        {
            if(i.idquestao == id)
            {
                _context.respostas.Remove(i);
            }
        }
    }
}