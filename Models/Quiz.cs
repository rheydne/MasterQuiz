using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class Quiz
    {
        [Key]
        public int id {get; set;}

        public int quantidadequestoes {get; set;}

        public int questoescorretas {get; set;}

        public TimeSpan tempoquiz {get; set;}

        public int materiaquiz {get; set;}

        public int idaluno {get; set;}
    }
}