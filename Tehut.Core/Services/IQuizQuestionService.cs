using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizQuestionService
    {
        /// <summary>
        /// Create a new empty question for the given quiz with the given question text. 
        /// </summary>
        /// <param name="quiz">The quiz, to which the new question should be attached.</param>
        /// <returns>The new created quiz question.</returns>
        Task<QuizQuestion> CreateQuestion(Quiz quiz);

        /// <summary>
        /// Saves the question by updating its values in the database.
        /// </summary>
        /// <param name="question">The question to be saved.</param>
        Task SaveQuestion(QuizQuestion question);

        /// <summary>
        /// Deletes the question and removes it from the attached quiz. 
        /// </summary>
        /// <param name="question">The question to be deleted.</param>
        Task DeleteQuestion(QuizQuestion question);
    }
}   
