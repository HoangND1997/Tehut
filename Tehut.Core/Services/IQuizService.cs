using Tehut.Core.Models;

namespace Tehut.Core.Services
{
    public interface IQuizService
    {
        #region  Write Methods 

        /// <summary>
        /// Creates a new quiz with the given title. 
        /// </summary>
        /// <param name="title">The title of the new quiz.</param>
        /// <returns>The newly created quiz.</returns>
        Task<Quiz> CreateQuiz(string title);

        /// <summary>
        /// Edits the title of the quiz to the new given title. 
        /// </summary>
        /// <param name="quiz">The quiz to be added.</param>
        /// <param name="newTitle">The new title of the quiz.</param>
        Task EditQuiz(Quiz quiz, string newTitle);

        /// <summary>
        /// Deletes the quiz. 
        /// </summary>
        /// <param name="quiz">Quiz, to be deleted.</param>
        Task DeleteQuiz(Quiz quiz);

        #endregion


        #region Search Methods

        /// <summary>
        /// Get all the available quizzes. 
        /// </summary>
        /// <returns>All available quizzes.</returns>
        Task<IEnumerable<Quiz>> GetAllQuizzes();

        /// <summary>
        /// Gets a quiz by its name or its title. 
        /// </summary>
        /// <param name="title">The name or title of the quiz to search for.</param>
        /// <returns>The quiz with the given title.</returns>
        Task<Quiz?> GetQuizByName(string title);

        #endregion

        /// <summary>
        /// Gets all the questions attached to the quiz. 
        /// </summary>
        /// <param name="quiz">The quiz to retrieve the questions from.</param>
        /// <returns>All question belonging to the quiz.</returns>
        Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz); 
    }
}
