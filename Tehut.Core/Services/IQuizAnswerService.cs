using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizAnswerService
    {
        /// <summary>
        /// Creates an answer to the given question with the given answer text. 
        /// </summary>
        /// <param name="question">The quiz question to which the answer is attached to.</param>
        /// <param name="answer">The text of the quiz answer.</param>
        Task<QuizAnswer> CreateAnswer(QuizQuestion question, string answer);

        /// <summary>
        /// Edits the text of the quiz answer to the new given text. 
        /// </summary>
        /// <param name="answer">The quiz answer to be edited.</param>
        /// <param name="newAnswer">The new text of the quiz answer.</param>
        /// <returns></returns>
        Task EditAnswer(QuizAnswer answer, string newAnswer); 

        /// <summary>
        /// Deletes the answer from the attached question.
        /// </summary>
        /// <param name="answer">The quiz answer to be deleted.</param>
        /// <returns></returns>
        Task DeleteAnswer(QuizAnswer answer); 
    }
}
