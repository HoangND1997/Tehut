using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public  interface IQuizRepository
    {
        #region  Write Methods 

        Task<Quiz> CreateQuiz(string name);

        Task<Quiz> RenameQuiz(Quiz quiz, string newName);

        Task DeleteQuiz(Quiz quiz);

        #endregion


        #region Search Methods

        Task<IEnumerable<Quiz>> GetAllQuiz();

        Task<IEnumerable<Quiz>> GetAllQuiz(string pattern);

        Task<Quiz> GetQuizByName(string name);

        Task<Quiz> GetQuizById(int quizId); 

        #endregion


        #region Question Methods

        Task AddQuestion(QuizQuestion question);

        Task RemoveQuestion(QuizQuestion question);

        #endregion 
    }
}
