using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public interface IQuizQuestionRepository
    {
        /// <summary>
        /// Creates a new empty question in the database for the given quiz. 
        /// </summary>
        /// <param name="quiz">The quiz to which the questions is added.</param>
        /// <returns>The newly created question.</returns>
        Task<QuizQuestion> CreateQuestion(Quiz quiz);

        /// <summary>
        /// Saves the question by updating its values in the database.
        /// </summary>
        /// <param name="question"> Question To be saved.</param>
        Task SaveQuestion(QuizQuestion question); 

        /// <summary>
        /// Deletes the quiz question from the database and removed the reference to the belonging quiz.
        /// </summary>
        /// <param name="question">The question to be removed.</param>
        Task DeleteQuestion(QuizQuestion question); 
    }
}
