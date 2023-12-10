using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizQuestionService
    {
        #region Question Handling 

        Task<QuizQuestion> CreateQuestion(string question);

        Task EditQuestion(QuizQuestion question, string newQuestion);

        #endregion


        #region Answer Handling 

        Task SetCorrectAnswer(QuizAnswer answer); 

        Task<QuizQuestion> AddAnswer(QuizAnswer answer); 
    
        Task DeleteAnswer(QuizAnswer answer);

        #endregion 
    }
}   
