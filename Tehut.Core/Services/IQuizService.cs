using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizService
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

        #endregion


        #region Question Methods

        Task AddQuestion(QuizQuestion question);

        Task RemoveQuestion(QuizQuestion question);

        #endregion 
    }
}
