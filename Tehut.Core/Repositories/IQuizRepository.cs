using System.Data.SqlTypes;
using Tehut.Core.Models;

namespace Tehut.Core.Repositories
{
    public  interface IQuizRepository
    {
        #region  Write Methods 

        /// <summary>
        /// Creates a new quiz in the database with the given title. 
        /// </summary>
        /// <param name="title">The title of the new quiz.</param>
        /// <returns>The new created quiz</returns>
        Task<Quiz> CreateQuiz(string title);

        /// <summary>
        /// Edits the title of the given quiz to the new given title in the database. 
        /// </summary>
        /// <param name="quiz">The quiz to be edited.</param>
        /// <param name="newName">The new title of the quiz.</param>
        Task EditQuiz(Quiz quiz, string newName);

        /// <summary>
        /// Deletes the quiz from the database. 
        /// </summary>
        /// <param name="quiz">The quiz to be deleted.</param>
        Task DeleteQuiz(Quiz quiz);

        #endregion


        #region Search Methods

        /// <summary>
        /// Gets all the quizzes available in the database. 
        /// </summary>
        /// <returns>All available quizzes.</returns>
        Task<IEnumerable<Quiz>> GetAllQuizzes();

        /// <summary>
        /// Gets a quiz by its title from the database.
        /// </summary>
        /// <param name="name">The title to search for in a quiz.</param>
        /// <returns>The quiz with the given title or null if not existing.</returns>
        Task<Quiz?> GetQuizByName(string name);

        /// <summary>
        /// Checks whether or not the given title already exists as a title for a quiz. 
        /// </summary>
        /// <param name="name">The title to check for existence.</param>
        /// <returns>True, if there is already a quiz with the given title.</returns>
        Task<bool> DoesQuizNameExists(string name); 

        #endregion

        /// <summary>
        /// Gets all the questions from the database that are belonging to the quiz.
        /// </summary>
        /// <param name="quiz">The quiz from which the questions should be retrieved.</param>
        /// <returns>All questions belonging to the quiz.</returns>
        Task<IEnumerable<QuizQuestion>> GetQuestions(Quiz quiz);
    }
}
