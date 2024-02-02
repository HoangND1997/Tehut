using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizService
    {
        #region  Write Methods 

        /// <summary>
        /// Creates a new quiz with the given name. 
        /// </summary>
        /// <param name="name">The name of the new quiz.</param>
        /// <returns>The newly created quiz.</returns>
        Task<Quiz> CreateQuiz(string name);

        /// <summary>
        /// Saves the quiz by updating its values in the database.
        /// </summary>
        /// <param name="quiz">The quiz to be saved.</param>
        Task SaveQuiz(Quiz quiz);

        /// <summary>
        /// Deletes the quiz. 
        /// </summary>
        /// <param name="quiz">Quiz, to be deleted.</param>
        Task DeleteQuiz(Quiz quiz);

        #endregion

        /// <summary>
        /// Get all the available quizzes. 
        /// </summary>
        /// <returns>All available quizzes.</returns>
        Task<IEnumerable<Quiz>> GetAllQuizzes();

        /// <summary>
        /// Loads all the questions and attaches it to the quiz. 
        /// </summary>
        /// <param name="quiz">The quiz to retrieve the questions from.</param>
        /// <returns>All question belonging to the quiz.</returns>
        Task LoadQuestionsFor(Quiz quiz); 
    }
}
