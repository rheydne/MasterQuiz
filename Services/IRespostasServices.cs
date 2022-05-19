using QuizApi.Models;

public interface IRespostasServices{

    List<Resposta> GetRespostasQuiz(List<int> idQuestao);

    List<Resposta> GetRespostasQuestao(int idQuestao);

    public void DeleteRepostas(int idQuestao);
}