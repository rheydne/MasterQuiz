using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class Resposta
    {
        [Key]
        public int id {get; set;}
        public string descricaoresposta {get; set;}
        public int idtcorreta {get; set;}
        public int idquestao { get; set; }
    }
}