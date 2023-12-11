using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizQuestionService
    {
        #region Question Handling 

        /// <summary>
        /// Create a new question for the given quiz with the given question text. 
        /// </summary>
        /// <param name="quiz">The quiz, to which the new question should be attached.</param>
        /// <param name="question">The text of the new question.</param>
        Task<QuizQuestion> CreateQuestion(Quiz quiz, string question);

        /// <summary>
        /// Edits the question with the new given text. 
        /// </summary>
        /// <param name="question">The question to be edited.</param>
        /// <param name="newQuestion">The new text of the question.</param>
        Task EditQuestion(QuizQuestion question, string newQuestion);

        /// <summary>
        /// Deletes the question and removes it from the attached quiz. 
        /// </summary>
        /// <param name="question">The question to be deleted.</param>
        /// <returns></returns>
        Task DeleteQuestion(QuizQuestion question);

        #endregion


        #region Answer Handling 

        /// <summary>
        /// Sets the given answer as the correct answer for the attached question. 
        /// </summary>
        /// <param name="answer">The quiz answer to be set as correct.</param>
        Task SetCorrectAnswer(QuizAnswer answer); 

        /// <summary>
        /// Gets all the possible answers of a question.
        /// </summary>
        /// <param name="question">The question, from which the answers should be queried.</param>
        /// <returns></returns>
        Task<IEnumerable<QuizAnswer>> GetAnswers(QuizQuestion question); 

        #endregion 
    }
}   
