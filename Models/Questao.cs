using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApi.Models
{
    public class Questao
    {
        [Key]
        public int id {get; set;}

        public string descricaoquestao {get; set;}

        public int idmateria {get; set;}
    }
}