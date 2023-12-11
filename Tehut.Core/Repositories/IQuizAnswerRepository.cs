using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public interface IQuizAnswerRepository
    {
        /// <summary>
        /// Creates an answer in the database and creates reference to the quiz reference.
        /// </summary>
        /// <param name="quizQuestion">The question to which the answer is attached.</param>
        /// <param name="answer">The text of the quiz answer.</param>
        /// <returns>The newly created answer.</returns>
        Task<QuizAnswer> CreateAnswer(QuizQuestion quizQuestion, string answer);

        /// <summary>
        /// Edits the text of the quiz answer in the database to the new given text.
        /// </summary>
        /// <param name="answer">The answer to be edited.</param>
        /// <param name="newAnswer">The new text of the answer.</param>
        Task EditAnswer(QuizAnswer answer, string newAnswer); 

        /// <summary>
        /// Deletes the answer in the database the removes reference to the quiz question.
        /// </summary>
        /// <param name="answer">Quiz answer that should be removed.</param>
        Task DeleteAnswer(QuizAnswer answer);   
    }
}
